﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Dashboard;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDeliveryRepository _rpsDelivery;
        private readonly IOrderService _srvOrder;
        private readonly ISaleService _srvSale;

        public DashboardController(ILogger<DashboardController> logger, IOrderService orderService, ISaleService saleService, IDeliveryRepository deliveryRepository)
        {
            _logger = logger;
            _srvOrder = orderService;
            _srvSale = saleService;
            _rpsDelivery = deliveryRepository;
        }

        public IActionResult Index()
        {
            //string auth_user = HttpContext.Session.GetString("auth_user");
            //if (auth_user == null)
            //    return View("~/Views/Shared/_SessionTimeout.cshtml");

            //var current_user = JsonConvert.DeserializeObject<UserAuthenticateDto>(auth_user);
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        public IActionResult Orders()
        {
            try
            {
                ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
                var ordersAvailable = _rpsDelivery.GetAll();
                return View(ordersAvailable ?? new List<OrderDeliveryDTO>());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new List<OrderDeliveryDTO>());
            }
        }

        public IActionResult SalesReport()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        public IActionResult NewSale()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        public IActionResult OrdersClient()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 1 };
            return View();
        }

        [HttpPost]
        public JsonResult NewSale([FromBody] PosSaleDto sale)
        {
            try
            {
                if(_srvSale.Save(sale))
                    return Json(JsonConvert.SerializeObject(new TransactionResponse { code = "000", message = "OK"}));
                else
                    return Json(JsonConvert.SerializeObject(new TransactionResponse { code = "001", message = "No se pudo registrar la venta."}));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(JsonConvert.SerializeObject(new TransactionResponse { code = "999", message = ex.Message }));
            }
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

        [HttpPost]
        public JsonResult GetSalesReport(string start_date, string end_date)
        {
            try
            {
                return Json(JsonConvert.SerializeObject(_srvOrder.GetSalesReport(start_date, end_date) ?? new ReportSales()));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new ReportSales());
            }
        }

        [HttpGet]
        public IActionResult GetDealers()
        {
            try
            {
                var dealers = _rpsDelivery.GetDealers();
                return Ok(JsonConvert.SerializeObject(dealers));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(new DealerDTO[0]);
            }
        }

    }
}
