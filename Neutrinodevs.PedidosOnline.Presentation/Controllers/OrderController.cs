using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _rpsOrder;
        public OrderController(IOrderRepository orderRepository)
        {
            _rpsOrder = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            var orders = _rpsOrder.GetAll();
            return Json(orders);
        }
    }
}
