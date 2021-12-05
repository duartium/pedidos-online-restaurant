using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Constants;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Domain.Models;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using Neutrinodevs.PedidosOnline.Infraestructure.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Neutrinodevs.PedidosOnline.Domain.Enums;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _rpsUser;
        private readonly IEmailService _rpsEmail;
        private readonly ILogger<UserController> _logger;

        public UserController(UserRepository userRepository, IEmailService emailService, ILogger<UserController> logger)
        {
            _rpsUser = userRepository;
            _rpsEmail = emailService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var usersDto = new List<UserMainDto>();
            var users = _rpsUser.GetAll();

            //TODO: reemplazar con automapper
            if(users != null)
            {
                usersDto = users.ToList().Select(x => 
                new UserMainDto { 
                    IdUsuario = x.IdUsuario,
                    Nombres = x.Nombres,
                    Username = x.Username,
                    TipoUsuario = CValues.Roles[x.TipoUsuario -1]
                }).ToList();
            }
            return View(usersDto);
        }

        public IActionResult New()
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

            if (_rpsUser.ValidateDuplicateUsername(user.Username))
                return Json("003");

            var userRegister = new UserRegisterDto();
            try
            {
                user.CodeEmailVerification = Password.Generate(6);
                user.Password = Security.GetSHA256(user.Password);
                int idClient = _rpsUser.Save(user);
                if (idClient > 0)
                {
                    string bodyHtml = CString.EMAIL_TEMPLATE.Replace("@code", user.CodeEmailVerification);

                    _rpsEmail.Send(new EmailParams
                    {
                        SenderEmail = "delivery.lapesca@gmail.com",
                        SenderName = "La Pesca",
                        Subject = "Bienvenido",
                        EmailTo = user.Email,
                        Body = bodyHtml
                    });

                    userRegister.Code = "000";
                    userRegister.IdClient = idClient;
                    return Json(userRegister);
                }
                else
                {
                    userRegister.Code = "001";
                    return Json(userRegister);
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                userRegister.Code = "001";
                return Json(userRegister);
            }
        }

        [HttpPost]
        public JsonResult Login(string username, string password, bool is_client)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                {
                    _logger.LogWarning($"Se esperaba {nameof(username)} y {nameof(password)}.");
                    return Json("002");
                }

                string passEncrypt = Security.GetSHA256(password.Trim());
                var auth = _rpsUser.Login(username.Trim(), passEncrypt, is_client);
                //HttpContext.Session.SetString("auth_user", JsonConvert.SerializeObject(auth));ç

                return Json(auth);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new UserAuthenticateDto { Code = "001"});
            }
        }

        [HttpPost]
        public JsonResult Verify([FromBody] UserRegisterDto user)
        {
            try
            {
                if (_rpsUser.Verify(user))
                    return Json("000");
                else
                    return Json("001");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json("001");
            }
        }

    }
}
