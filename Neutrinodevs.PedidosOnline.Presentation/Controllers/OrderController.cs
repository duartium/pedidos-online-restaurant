using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Models;
using System;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _srvOrder;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _srvOrder = orderService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Processing(int id_order)
        {
            var currentOrder = _srvOrder.Get(id_order);
            return View(currentOrder);
        }

        public JsonResult GetAll()
        {
            try
            {
                var orders = _srvOrder.GetAll();
                return Json(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("error");
            }
        }

        public JsonResult Save([FromBody] FullOrderDto order_invoice)
        {
            try
            {
                int idOrder = _srvOrder.Save(order_invoice);
                if(idOrder > 0)
                    return Json(new OrderResponse { IdOrder = idOrder, Code = "000" });
                else
                    return Json(new OrderResponse { IdOrder = -1, Code = "001" });
            }
            catch (Exception)
            {
                return Json(new OrderResponse { IdOrder = -1, Code = "001" });
            }
        }

        [HttpPost]
        public JsonResult AssignDelivery([FromBody] int id_order, int id_employee)
        {
            try
            {
                if (id_order <= 0) throw new ArgumentNullException(nameof(id_order));
                if (id_order <= 0) throw new ArgumentNullException(nameof(id_employee));

                bool transaction = _srvOrder.AssignDelivery(id_order, id_employee);
                
                return Json(new TransactionResponse { 
                    code = transaction ? "000" : "001", 
                    message = !transaction ? "No se pudo realizar la asignación." : String.Empty
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new TransactionResponse { code = "001" });
            }
        }

        public IActionResult Checkout()
        {
            return View();
        }

    }
}
