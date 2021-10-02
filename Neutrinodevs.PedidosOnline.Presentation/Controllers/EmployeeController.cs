using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _rpsEmployee = employeeRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var employees = _rpsEmployee.GetAll();
            var epmloyeesDto = _mapper.Map<EmployeeDto>(employees.ToList());
            return View();
        }
    }
}
