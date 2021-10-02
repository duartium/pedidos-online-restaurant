using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Presentation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _rpsEmployee;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _rpsEmployee = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var employees = _rpsEmployee.GetAll();
                var epmloyeesDto = _mapper.Map<List<EmployeeDto>>(employees.ToList());
                return View(epmloyeesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }
    }
}
