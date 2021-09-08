using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using System;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _rpsUser;
        public UserController(UserRepository userRepository)
        {
            _rpsUser = userRepository;
        }
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
            if(_rpsUser.ValidateDuplicateUser(user.Identification))
                return Json("002");

            if (_rpsUser.Save(user))
                return Json("000");
            else
                return Json("001");
        }

        [HttpPost]
        public JsonResult Login()
        {
            return Json("login");
        }

        [HttpPost]
        public JsonResult Athenticate(string username, string password)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                {
                    //Utils.WriteLog("Athenticate", "Se esperaba el username y password.");
                    return Json("002");
                }

                return Json("000");
            }
            catch (Exception ex)
            {
                //Utils.WriteLog("Athenticate", ex.Message);
                return Json("001");
            }
        }

    }
}
