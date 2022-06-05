using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _rpsCustomer;
        private readonly IOrderRepository _rpsOrder;
        private readonly ILogger<CustomerController> _logger;
        private readonly UserRepository _rpsUser;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger, 
            UserRepository userRepository, IOrderRepository orderRepository)
        {
            _rpsCustomer = customerRepository;
            _logger = logger;
            _rpsUser = userRepository;
            _rpsOrder = orderRepository;
        }

        public IActionResult Index()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            var customers = _rpsCustomer.GetAll();
            return View(customers);
        }

        [Route("New")]
        public IActionResult NewCustomer()
        {
            ViewBag.User = new UserAuthenticateDto { IdRole = 4 };
            return View();
        }

        [HttpPost]
        public IActionResult Get(string identification)
        {
            try
            {
                var client = _rpsCustomer.Get(identification);
                return Ok(JsonConvert.SerializeObject(client));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save(PosSaleDto sale)
        {
            try
            {
                var client = new PosSaleDto();
                return Ok(JsonConvert.SerializeObject(client));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Register([FromBody] CustomerSaveDTO user)
        {
            if (_rpsUser.ValidateDuplicateUser(user.Identification))
                return Json("002");

            var userRegister = new UserRegisterDto();
            try
            {
                if (_rpsCustomer.Save(user))
                {
                    userRegister.Code = "000";
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

        
        [HttpGet]
        public IActionResult GetOrdersHistory(int idCustomer)
        {
            try
            {
                var orders = _rpsOrder.GetAllByCustomer(idCustomer);
                return Ok(JsonConvert.SerializeObject(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(ex.Message);
            }
        }
    }
}
