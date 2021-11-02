using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public FinalOrderDto Get(int idOrder)
        {
            return _orderRepository.Get(idOrder);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public int Save(FullOrderDto order)
        {
            return _orderRepository.Save(order);
        }

        public bool AssignDelivery(int idOrder, int idEmployee)
        {
            return _orderRepository.AssignDelivery(idOrder, idEmployee);
        }

        public bool SetOrderStage(int idOrder, int idStage)
        {
            return _orderRepository.SetOrderStage(idOrder, idStage);
        }

        
    }
}
