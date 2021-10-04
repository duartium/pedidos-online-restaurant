using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _rpsDelivery;
        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            _rpsDelivery = deliveryRepository;
        }

        public IActionResult Index()
        {
            var ordersAvailable = _rpsDelivery.GetAll();
            return View(ordersAvailable ?? new List<OrderDeliveryDTO>());
        }
    }
}
