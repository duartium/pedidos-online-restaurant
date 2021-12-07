using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Models;
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
        bool SetOrderStage(int idOrder, int idStage);
        bool FinishOrder(int idOrder, int idStage);
        bool Cancel(CancelOrder cancel);
        int GetStage(int idOrder);
    }
}
