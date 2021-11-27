using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IOrderService _srvOrder;

        public DashboardController(ILogger<DashboardController> logger, IOrderService orderService)
        {
            _logger = logger;
            _srvOrder = orderService;
        }

        public IActionResult Index()
        {
            //string auth_user = HttpContext.Session.GetString("auth_user");
            //if (auth_user == null)
            //    return View("~/Views/Shared/_SessionTimeout.cshtml");

            //var current_user = JsonConvert.DeserializeObject<UserAuthenticateDto>(auth_user);

            return View();
        }


        [HttpPost]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _srvOrder.GetAll() ?? new List<Order>(); ;
                return Ok(JsonConvert.SerializeObject(orders));
            }
            catch (System.Exception  ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(new List<Order>());
            }
        }

    }
}
