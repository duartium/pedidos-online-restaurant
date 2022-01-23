using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _rpsDelivery;
        private readonly ILogger<DeliveryController> _logger;
        public DeliveryController(IDeliveryRepository deliveryRepository, ILogger<DeliveryController> logger)
        {
            _rpsDelivery = deliveryRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.User = new UserAuthenticateDto { IdRole = 3 };
                var ordersAvailable = _rpsDelivery.GetAll();
                return View(ordersAvailable ?? new List<OrderDeliveryDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new List<OrderDeliveryDTO>());
            }
        }

        public IActionResult DailyReport(int idDealer)
        {
            try
            {
                ViewBag.User = new UserAuthenticateDto { IdRole = 3 };
                //var dailySales = _rpsDelivery.GetDailySales(idDealer);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new List<OrderDeliveryDTO>());
            }
        }

        public IActionResult MyDeliveries()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 3 };
            return View();
        }

        [HttpPost]
        public JsonResult GetAll()
        {
            try
            {
                return Json(JsonConvert.SerializeObject(_rpsDelivery.GetAll() ?? new List<OrderDeliveryDTO>())); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("001");
            }
        }

        [HttpPost]
        public JsonResult GetDeliveries(int idDealer)
        {
            try
            {
                return Json(JsonConvert.SerializeObject(_rpsDelivery.GetDeliveriesByDealer(idDealer) ?? new List<OrderDeliveryDTO>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("001");
            }
        }

        [HttpPost]
        public JsonResult GetDailyReport(int idDealer)
        {
            try
            {
                return Json(JsonConvert.SerializeObject(_rpsDelivery.GetDailySales(idDealer) ?? new DailyOrdersDelivery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("001");
            }
        }

    }
}
