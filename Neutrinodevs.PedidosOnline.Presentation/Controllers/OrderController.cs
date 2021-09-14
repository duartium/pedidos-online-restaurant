using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using System;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _srvOrder;
        public OrderController(IOrderService orderService)
        {
            _srvOrder = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Processing(int id_order)
        {

            return View();
        }

        public JsonResult GetAll()
        {
            try
            {
                var orders = _srvOrder.GetAll();
                return Json(orders);
            }
            catch (System.Exception)
            {
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

        public IActionResult Checkout()
        {
            return View();
        }

    }
}
