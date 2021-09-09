using Microsoft.AspNetCore.Mvc;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Domain.Models;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using Neutrinodevs.PedidosOnline.Infraestructure.Security;
using System;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _rpsUser;
        private readonly IEmailService _rpsEmail;
        public UserController(UserRepository userRepository, IEmailService emailService)
        {
            _rpsUser = userRepository;
            _rpsEmail = emailService;
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


            try
            {
                user.CodeEmailVerification = Password.Generate(6);
                if (_rpsUser.Save(user))
                {
                    string bodyHtml = "<div style='background-color: #f8f5f1; color: #555; width: 80%; height:360px; margin: 0 auto; border-bottom: 5px solid #325288; font-family: Arial, Helvetica, sans-serif;'>"
                                + "<div style = 'background-color: #325288; height:70px; padding:3px;'>"
                                     + "<h2 style = 'color:#fff; text-align:center'>"
                                        + "Verificación de correo electrónico"
                                    + "<h2>"
                                + "</div>"
                                + "<section style = 'padding: 20px;'>"
                                    + "<p style='font-size:20px;line-height:1.9rem'>Código de verificación:</p>"
                                        + "<div style = 'font-size: 22px; border-radius: 25px; background-color:#fff;font-weight:bold;text-align:center;width:80%; margin: 0 auto;'>"
                                        + $"<p style='padding:5px 5px 15px'>{user.CodeEmailVerification} </p>"
                                    + "</ div >"
                                + "</section>"
                                + "</div>";

                    _rpsEmail.Send(new EmailParams
                    {
                        SenderEmail = "delivery.lapesca@gmail.com",
                        SenderName = "La Pesca",
                        Subject = "Bienvenido",
                        EmailTo = user.Email,
                        Body = bodyHtml
                    });
                    return Json("001");
                }
                else
                    return Json("001");
            }
            catch (Exception ex)
            {
                return Json("001");
            }
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
