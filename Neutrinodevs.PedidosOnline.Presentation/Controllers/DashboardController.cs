using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string auth_user = HttpContext.Session.GetString("auth_user");
             if (auth_user == null)
                return View("~/Views/Shared/_SessionTimeout.cshtml");

            var current_user = JsonConvert.DeserializeObject<UserAuthenticateDto>(auth_user);
            return View(current_user);
        }
    }
}
