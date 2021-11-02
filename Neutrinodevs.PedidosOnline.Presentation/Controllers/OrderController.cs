using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
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
        public JsonResult AssignDelivery([FromBody] AssignDeliveryDTO assignDeliveryDTO) 
        {
            try
            {
                if (assignDeliveryDTO == null) throw new ArgumentNullException(nameof(assignDeliveryDTO));
                if (assignDeliveryDTO.IdEmployee <= 0) throw new ArgumentNullException(nameof(assignDeliveryDTO.IdEmployee));
                if (assignDeliveryDTO.IdOrder <= 0) throw new ArgumentNullException(nameof(assignDeliveryDTO.IdEmployee));

                bool transaction = _srvOrder.AssignDelivery(assignDeliveryDTO.IdOrder, assignDeliveryDTO.IdEmployee);
                var json = new TransactionResponse
                {
                    code = transaction ? "000" : "001",
                    message = !transaction ? "No se pudo realizar la asignación." : "La orden ha sido asignada a usted."
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new TransactionResponse { code = "001" });
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public JsonResult SetOrderStage([FromForm]int idOrder, int idStage)
        {
            try
            {
                if (idOrder == 0) throw new ArgumentNullException(nameof(idOrder));

                bool transaction = _srvOrder.SetOrderStage(idOrder, idStage);
                return Json(new TransactionResponse
                {
                    code = transaction ? "000" : "001",
                    message = !transaction ? "No se pudo cambiar la estapa del pedido." : String.Empty
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
