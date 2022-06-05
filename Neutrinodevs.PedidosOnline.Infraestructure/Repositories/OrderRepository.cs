using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using Neutrinodevs.PedidosOnline.Domain.Models;
using Neutrinodevs.PedidosOnline.Infraestructure.Hubs.Hubs;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Neutrinodevs.PedidosOnline.Domain.Constants;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Dashboard;
using System.Globalization;
using Neutrinodevs.PedidosOnline.Infraestructure.Extensions.Common;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using Newtonsoft.Json;

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

        //public IEnumerable<Order> GetAll()
        //{
        //    try
        //    {
        //        List<Order> lsOrders = null;
        //        Order oOrder = null;
        //        SqlDependency dependency = null;

        //        string connString = _configuration.GetConnectionString("pedidos_online");
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //                SqlCommand cmd = new SqlCommand("ND_SP_PRODUCTOS_MANT", conn);
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                #region SQL Dependency

        //                dependency = new SqlDependency(cmd);
        //                dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
        //                SqlDependency.Start(connString);

        //                #endregion
        //                //TODO: mapping de datos de cotización

        //                SqlDataReader dr = cmd.ExecuteReader();
        //                if (dr.HasRows)
        //                {
        //                    lsOrders = new List<Order>();
        //                    while (dr.Read())
        //                    {
        //                        oOrder = new Order
        //                        {
        //                            IdOrder = dr.IsDBNull(dr.GetOrdinal("id_producto")) ? -1 : dr.GetInt32(dr.GetOrdinal("id_producto")),
        //                            CustomerName = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre")),
        //                            TotalAmount = dr.IsDBNull(dr.GetOrdinal("precio")) ? decimal.Zero : dr.GetDecimal(dr.GetOrdinal("precio")),
        //                            DateTime = "2021/08/02 00:00:00"
        //                        };
        //                        lsOrders.Add(oOrder);
        //                    }
        //                }
        //                return lsOrders;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public IEnumerable<Order> GetAll()
        {
            //var orders = (from ped in _context.Pedidos
            //              join det in _context.PedidoDetalle
            //              on ped.IdPedido equals det.PedidoId
            //              where ped.Estado == 1 && det.Estado == 1
            //              group new { det, ped } by new { det.PedidoId } into finalProduct
            //              orderby finalProduct.Key descending
            //              select new OrderReviewDto { 
            //                   IdOrder = finalProduct.Key.PedidoId,
            //                   Number = finalProduct.First().ped.Numero,
            //                   TotalAmount = finalProduct.Sum(x => x.det.Total)
            //              }).ToList();

            var orders = _context.Pedidos.Where(x => x.Estado == 1)
                .Select(x => new Order{ 
                    IdOrder = x.IdPedido,
                    TotalAmount = Convert.ToDecimal(x.Total, System.Globalization.CultureInfo.InvariantCulture),
                    DateTime = x.Fecha.ToString("dd-MM-yyyy HH:mm"),
                    Number = x.Numero.ToString().PadLeft(7, '0'),
                    IdStage = (int)x.Stage,
                    Stage = CValues.Stages[(int)x.Stage -1]
                }).OrderByDescending(x => x.IdOrder)
                .ToList();
            return orders;
        }

        public FinalOrderDto Get(int idOrder)
        {
            try
            {
                var fullOrder = (from ped in _context.Pedidos
                                 join det in _context.PedidoDetalle on ped.IdPedido equals det.PedidoId
                                 join cl in _context.Clientes on ped.ClienteId equals cl.IdCliente
                                 join emp in _context.Empleados on ped.DeliveryId equals emp.IdEmpleado
                                 into subemp from emp in subemp.DefaultIfEmpty()
                                 where ped.Estado == 1 && det.Estado == 1 && ped.IdPedido == idOrder
                                 select new FinalOrderDto
                                 {
                                     IdOrder = ped.IdPedido,
                                     CustomerName = cl.GetFullNames(),
                                     DealerName = emp.Nombres,
                                     DeliveryTime = ped.DeliveryTime,
                                     Number = ped.Numero.ToString().PadLeft(6, '0'),
                                     Iva = Convert.ToDecimal(ped.Iva, CultureInfo.InvariantCulture),
                                     Total = Convert.ToDecimal(ped.Total, CultureInfo.InvariantCulture),
                                     Subtotal = Convert.ToDecimal(ped.Subtotal, CultureInfo.InvariantCulture)
                                 }).FirstOrDefault();

                var details = (from det in _context.PedidoDetalle
                               where det.PedidoId == idOrder && det.Estado == 1
                               select new Item{ 
                                    id = det.IdPedidoDet,
                                    name = det.NombreProducto,
                                    price = Convert.ToDecimal(det.Total, System.Globalization.CultureInfo.InvariantCulture),
                                    quantity = det.Cantidad
                               }).ToList();

                //fullOrder.DealerName = _context.Empleados.Where(x => x.TipoEmpleado == (int)TipoEmpleado.Repartidor)
                //    && x.
                //    .Select(x => $"{x.Nombres} {x.Apellidos}").FirstOrDefault();

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
                        DeliveryTime = DateTime.Now.AddMinutes(30).ToString("HH:mm"),
                        Subtotal = order.subtotal,
                        Iva = order.iva,
                        Total = order.total,
                        Stage = 1,
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

        public bool SetOrderStage(int idOrder, int idStage)
        {
            var order = _context.Pedidos.Single(x => x.IdPedido == idOrder);
            order.Stage = idStage;
            _context.Entry(order).Property(x => x.Stage).IsModified = true;

            int value = _context.SaveChanges();
            return (value > 0);
        }

        public bool FinishOrder(int idOrder, int idStage)
        {
            int result = 0;
            using (_context.Database.BeginTransaction())
            {
                int idDealer = 0;

                //se cambia a etapa: Entregado
                var order = _context.Pedidos.Single(x => x.IdPedido == idOrder);
                order.Stage = idStage;
                idDealer = (int)order.DeliveryId;

                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                //se genera un nuevo secuancial
                var secuencialRow = _context.Secuenciales.Where(x => x.Nombre.Equals("factura"))
                    .Select(x => x).FirstOrDefault();

                int nuevoSecuencial = (int)secuencialRow.Secuencial + 1;
                secuencialRow.Secuencial = nuevoSecuencial;
                _context.Update(secuencialRow);
                _context.SaveChanges();

                var comprobante = new ComprobanteVenta
                {
                    Fecha = DateTime.Now.ToUniversalTime(),
                    Estado = (int)Estado.Activo,
                    Numero = nuevoSecuencial,
                    Subtotal = (decimal)order.Subtotal,
                    Iva = order.Iva,
                    Total = (decimal)order.Total,
                    EsPos = false
                };
                _context.ComprobanteVenta.Add(comprobante);
                _context.SaveChanges();

                //se obtiene los ids de los detalles del pedido
                List<int> ids = _context.PedidoDetalle.Where(x => x.Estado == 1 && x.PedidoId == idOrder)
                    .Select(x => x.IdPedidoDet).ToList();

                //se asigna los items del pedido al comprobante de venta
                var detallesComprobante = new List<ComprobanteDetalle>();
                foreach (var idItem in ids)
                {
                    var detalle = new ComprobanteDetalle
                    {
                        ComprobanteId = comprobante.IdComprobante,
                        ItemId = idItem,
                        Estado = (int)Estado.Activo
                    };
                    detallesComprobante.Add(detalle);
                }
                _context.ComprobanteDetalle.AddRange(detallesComprobante);
                result = _context.SaveChanges();

                //Se cambia el estado de actividad del dealer a Disponible
                var empleado = _context.Empleados.Single(x => x.IdEmpleado == idDealer);
                empleado.EstadoActividad = 1;//Disponible
                _context.Entry(empleado).State = EntityState.Modified;
                _context.SaveChanges();


                _context.Database.CommitTransaction();
            }

            return (result > 0);
        }

        public OrderResumeDTO GetOrderResume(int idOrder)
        {
            var orderResume = _context.Pedidos.Where(x => x.Estado == 1 && x.IdPedido == idOrder)
                .Select(x => new OrderResumeDTO { 
                    CustomerEmail = x.Cliente.Email,
                    Subtotal = (decimal)x.Subtotal,
                    Total = (decimal)x.Total,
                    Iva = (decimal)x.Iva
                }).FirstOrDefault();
            return orderResume;
        }

        public bool Cancel(CancelOrder cancel)
        {
            int resp = 0;
            using (var trans = _context.Database.BeginTransaction())
            {
                int idDealer = 0;

                var order = _context.Pedidos.Where(x => x.Estado == 1 && x.IdPedido == cancel.IdOrder).FirstOrDefault();
                order.MotivoRechazo = cancel.Reason;
                order.Stage = cancel.IdStage;
                idDealer = (int)order.DeliveryId;
                _context.Update(order);

                //Se cambia el estado de actividad del dealer a Disponible
                var empleado = _context.Empleados.Single(x => x.IdEmpleado == idDealer);
                empleado.EstadoActividad = 1;//Disponible
                _context.Entry(empleado).State = EntityState.Modified;
                
                resp = _context.SaveChanges();

                if (resp > 0)
                    trans.Commit();
                else
                    trans.Rollback();
            }

            return (resp > 0);
        }

        public int GetStage(int idOrder)
        {
            return _context.Pedidos.Where(x => x.Estado == 1 && x.IdPedido == idOrder)
                .Select(x => (int)x.Stage).FirstOrDefault();
        }

        public ReportSales GetSalesReport(string startDate, string endDate)
        {
            DateTime dStartDate = new DateTime(int.Parse(startDate.Split('-').GetValue(2).ToString()), 
                int.Parse(startDate.Split('-').GetValue(1).ToString()), int.Parse(startDate.Split('-').GetValue(0).ToString()));

            DateTime dEndDate = new DateTime(int.Parse(endDate.Split('-').GetValue(2).ToString()),
                int.Parse(endDate.Split('-').GetValue(1).ToString()), int.Parse(endDate.Split('-').GetValue(0).ToString()));

            ReportSales orders = new ReportSales();
            //orders.Items = _context.Pedidos.Where(x => x.Estado == 1 && x.Stage == 4 
            //        && x.Fecha.Date >= dStartDate.Date && x.Fecha.Date <= dEndDate.Date)
            //                               .Select(pedido => new ItemSale
            //                               {
            //                                   Number = pedido.Numero.ToString(),
            //                                   Time = pedido.DeliveryTime,
            //                                   Total = decimal.Parse(pedido.Total.ToString(), CultureInfo.InvariantCulture)
            //                               }).ToList();

            orders.Items = (from ped in _context.Pedidos
                          join emp in _context.Empleados
                          on ped.DeliveryId equals emp.IdEmpleado
                          join user in _context.Usuarios
                          on emp.UsuarioId equals user.IdUsuario
                          where ped.Estado == 1 && ped.Stage == (int)EtapaPedido.Entregado
                          && ped.Fecha.Date >= dStartDate.Date && ped.Fecha.Date <= dEndDate.Date
                          select new ItemSale
                              {
                              Number = ped.Numero.ToString().PadLeft(7, '0'),
                              Date = ped.Fecha.ToString("dd-MM-yyyy HH:mm"),
                              Total = decimal.Parse(ped.Total.ToString(), CultureInfo.InvariantCulture),
                              TipoVenta = "Online",
                              UserDelivery = user.Username
                          }).ToList();


            var salesPOS = (from fac in _context.ComprobanteVenta
                            where fac.Estado == 1
                            && fac.Fecha.Date >= dStartDate.Date && fac.Fecha.Date <= dEndDate.Date
                            select new ItemSale
                            {
                                Number = fac.Numero.ToString().PadLeft(7, '0'),
                                Date = fac.Fecha.ToString("dd-MM-yyyy HH:mm"),
                                Total = decimal.Parse(fac.Total.ToString(), CultureInfo.InvariantCulture),
                                TipoVenta = "Punto de venta",
                                UserDelivery = "Administrador"
                            }).ToList();

            orders.Items.AddRange(salesPOS);

            orders.Total = Math.Round(orders.Items.Select(x => x.Total).Sum(), 2);
            orders.SuccessfulDeliveries = orders.Items.Count;

            return orders;
        }


        public IEnumerable<OrderDeliveryDTO> GetAllByCustomer(int idCustomer)
        {
            try
            {
                List<OrderDeliveryDTO> orders = null;
                orders = _context.Pedidos.Where(x => x.Estado == 1 && x.Stage == 4)
                                               .Include(cl => cl.Cliente)
                                               .Include(det => det.PedidoDetalle)
                                               .Where(ped => ped.Estado == 1 && ped.ClienteId == idCustomer
                                               && ped.PedidoDetalle.Any(o => o.Estado == 1))
                                               .Select(pedido => new OrderDeliveryDTO
                                               {
                                                   IdOrder = pedido.IdPedido,
                                                   IdStage = (int)pedido.Stage,
                                                   Number = pedido.Numero.ToString().PadLeft(5, '0'),
                                                   Address = pedido.Cliente.Direccion,
                                                   DeliveryTime = pedido.DeliveryTime,
                                                   CustomerName = pedido.Cliente.Nombres + " " + pedido.Cliente.Apellidos,
                                                   CellphoneNumber = pedido.Cliente.Telefono,
                                                   Subtotal = decimal.Parse(pedido.Subtotal.ToString(), CultureInfo.InvariantCulture),
                                                   Total = decimal.Parse(pedido.Total.ToString(), CultureInfo.InvariantCulture),
                                                   JsonProducts = JsonConvert.SerializeObject(pedido.PedidoDetalle)
                                               }).ToList();
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

    }
}
