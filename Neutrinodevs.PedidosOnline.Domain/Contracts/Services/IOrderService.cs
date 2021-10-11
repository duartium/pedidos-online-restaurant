using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Services
{
    public interface IOrderService : IDisposable
    {
        IEnumerable<Order> GetAll();
        FinalOrderDto Get(int idOrder);
        int Save(FullOrderDto order);
        bool AssignDelivery(int idOrder, int idEmployee);
    }
}
