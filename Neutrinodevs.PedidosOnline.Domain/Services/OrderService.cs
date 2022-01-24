using Neutrinodevs.PedidosOnline.Domain.Constants;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Dashboard;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Models;
using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _srvEmail;

        public OrderService(IOrderRepository orderRepository, IEmailService emailService)
        {
            this._orderRepository = orderRepository;
            _srvEmail = emailService;
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

        public bool FinishOrder(int idOrder, int idStage)
        {

            bool resp = _orderRepository.FinishOrder(idOrder, idStage);

            var orderResume = _orderRepository.GetOrderResume(idOrder);

            string bodyHtml = CString.RECIBO_TEMPLATE.Replace("@total", orderResume.Total.ToString())
                .Replace("@subtotal", orderResume.Subtotal.ToString())
                .Replace("@iva", orderResume.Iva.ToString());

            string emailResp = "";
            _srvEmail.Send(new EmailParams
            {
                SenderEmail = "delivery.lapesca@gmail.com",
                SenderName = "La Pesca",
                Subject = "Recibo de su pedido.",
                EmailTo = orderResume.CustomerEmail,
                Body = bodyHtml
            }, out emailResp);

            return resp;   
        }

        public bool Cancel(CancelOrder cancel)
        {
            return _orderRepository.Cancel(cancel);
        }

        public int GetStage(int idOrder)
        {
            return _orderRepository.GetStage(idOrder);
        }

        public ReportSales GetSalesReport(string startDate, string endDate)
        {
            return _orderRepository.GetSalesReport(startDate, endDate);
        }
    }
}
