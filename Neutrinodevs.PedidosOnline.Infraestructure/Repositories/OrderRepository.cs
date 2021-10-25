using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using Neutrinodevs.PedidosOnline.Infraestructure.Hubs.Hubs;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ND_PEDIDOS_ONLINEContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(IConfiguration configuration, IHubContext<OrderHub> orderHub, 
                               ND_PEDIDOS_ONLINEContext context,    ILogger<OrderRepository> logger)
        {
            this._configuration = configuration;
            _orderHub = orderHub;
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Order> GetAll()
        {
            try
            {
                List<Order> lsOrders = null;
                Order oOrder = null;
                SqlDependency dependency = null;

                string connString = _configuration.GetConnectionString("pedidos_online");
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("ND_SP_PRODUCTOS_MANT", conn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        #region SQL Dependency
                        
                        dependency = new SqlDependency(cmd);
                        dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                        SqlDependency.Start(connString);

                        #endregion
                        //TODO: mapping de datos de cotización

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lsOrders = new List<Order>();
                            while (dr.Read())
                            {
                                oOrder = new Order
                                {
                                    IdOrder = dr.IsDBNull(dr.GetOrdinal("id_producto")) ? -1 : dr.GetInt32(dr.GetOrdinal("id_producto")),
                                    CustomerName = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre")),
                                    TotalAmount = dr.IsDBNull(dr.GetOrdinal("precio")) ? decimal.Zero : dr.GetDecimal(dr.GetOrdinal("precio")),
                                    DateTime = "2021/08/02 00:00:00"
                                };
                                lsOrders.Add(oOrder);
                            }
                        }
                        return lsOrders;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public FinalOrderDto Get(int idOrder)
        {
            try
            {
                var fullOrder = (from ped in _context.Pedidos
                                 join det in _context.PedidoDetalle on ped.IdPedido equals det.PedidoId
                                 where ped.Estado == 1 && det.Estado == 1 && ped.IdPedido == idOrder
                                 select new FinalOrderDto
                                 {
                                     IdOrder = ped.IdPedido,
                                     DeliveryTime = ped.DeliveryTime,
                                     Number = ped.Numero.ToString().PadLeft(8, '0'),
                                     Iva = (decimal)ped.Iva,
                                     Total = (decimal) ped.Total,
                                     Subtotal = (decimal)ped.Subtotal
                                 }).FirstOrDefault();

                var details = (from det in _context.PedidoDetalle
                               where det.PedidoId == idOrder && det.Estado == 1
                               select new Item{ 
                                    id = det.IdPedidoDet,
                                    name = det.NombreProducto,
                                    price = det.Total,
                                    quantity = det.Cantidad
                               }).ToList();
                
                fullOrder.items = details;
                return fullOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                _orderHub.Clients.All.SendAsync("UpdateOrdersTable");
            }
            GetAll();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public int Save(FullOrderDto order)
        {
            try
            {
                int idPedido = -1;

                using (_context.Database.BeginTransaction())
                {
                    //generación de secuencial de pedido
                    int nuevoSecuencial = (int)_context.Secuenciales.Where(x => x.Nombre.Equals("pedido"))
                                     .Select(x => x.Secuencial).FirstOrDefault() + 1;

                    var pedido = new Pedidos
                    {
                        ClienteId = order.id_client,
                        Fecha = DateTime.Now,
                        Numero = nuevoSecuencial,
                        DeliveryTime = DateTime.Now.ToString("HH:mm"),
                        Subtotal = order.subtotal,
                        Iva = order.iva,
                        Total = order.total,
                        Estado = 1
                    };
                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();
                    idPedido = pedido.IdPedido;

                    var pedidoDetalle = new List<PedidoDetalle>();
                    foreach (var item in order.items)
                    {
                        var orderItem = new PedidoDetalle
                        {
                            Cantidad = item.quantity,
                            NombreProducto = item.name,
                            ProductoId = item.id,
                            Total = item.price,
                            PedidoId = idPedido,
                            Estado = 1
                        };
                        pedidoDetalle.Add(orderItem);
                    }
                    _context.PedidoDetalle.AddRange(pedidoDetalle);
                    _context.SaveChanges();

                    //establece el nuevo secuencial
                    var secuencialRow = _context.Secuenciales.First(x => x.Nombre.Equals("pedido"));
                    secuencialRow.Secuencial = nuevoSecuencial;
                    _context.Secuenciales.Update(secuencialRow);
                    _context.SaveChanges();

                    _context.Database.CommitTransaction();
                }
                return idPedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }
        
        public bool AssignDelivery(int idOrder, int idEmployee)
        {
            var order = _context.Pedidos.Single(x => x.IdPedido == idOrder);
            order.DeliveryId = idEmployee;
            order.Stage = (int)EtapaPedido.EnPreparacion;
            _context.Entry(order).Property(x => x.Stage).IsModified = true;
            _context.Entry(order).Property(x => x.DeliveryId).IsModified = true;

            var employee = _context.Empleados.Single(x => x.IdEmpleado == idEmployee);
            employee.EstadoActividad = 2;
            _context.Entry(employee).Property(x => x.EstadoActividad).IsModified = true;
            
            int value = _context.SaveChanges();
            return (value > 0);
        }

        public bool SetOrderStage(int idOrder)
        {
            var order = _context.Pedidos.Single(x => x.IdPedido == idOrder);
            order.Stage = (int)EtapaPedido.EnCamino;
            _context.Entry(order).Property(x => x.Stage).IsModified = true;

            int value = _context.SaveChanges();
            return (value > 0);
        }

    }
}
