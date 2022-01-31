using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
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
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _rpsCustomer = customerRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
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
                var client = _rpsCustomer.Save(sale);
                return Ok(JsonConvert.SerializeObject(client));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Conflict(ex.Message);
            }
        }

    }
}
