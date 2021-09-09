using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;

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

        public IActionResult Processing()
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

        public IActionResult Checkout()
        {
            return View();
        }

    }
}
