using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Infraestructure.Hubs.Hubs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<OrderHub> _orderHub;

        public OrderRepository(IConfiguration configuration, IHubContext<OrderHub> orderHub)
        {
            this._configuration = configuration;
            _orderHub = orderHub;
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

    }
}
