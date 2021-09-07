using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verification()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register([FromBody] UserDto user)
        {
            return Json("register");
        }

        [HttpPost]
        public JsonResult Login()
        {
            return Json("login");
        }

    }
}
