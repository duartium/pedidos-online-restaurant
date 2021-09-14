using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
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

        public FullOrderDto Get(int idOrder)
        {
            try
            {
                var fullOrder = (from ped in _context.Pedidos
                            join det in _context.PedidoDetalle on ped.IdPedido equals det.PedidoId
                            where ped.Estado == 1 && det.Estado == 1
                            select new FullOrderDto
                            {
                                
                            }).FirstOrDefault();
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
                        Estado = 1
                    };
                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();
                    idPedido = pedido.IdPedido;

                    PedidoDetalle pedidoDetalle = null;
                    foreach (var item in order.items)
                    {
                        pedidoDetalle = new PedidoDetalle
                        {
                            Cantidad = item.quantity,
                            NombreProducto = item.name,
                            ProductoId = item.id,
                            Total = item.price,
                            PedidoId = idPedido,
                            Estado = 1
                        };
                    }
                    _context.PedidoDetalle.Add(pedidoDetalle);
                    _context.SaveChanges();

                    //establece el nuevo secuencial
                    var secuencialRow = _context.Secuenciales.Where(x => x.Nombre.Equals("pedido"))
                                     .Select(x => x).FirstOrDefault();
                    secuencialRow.Secuencial = nuevoSecuencial;
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
        

    }
}
