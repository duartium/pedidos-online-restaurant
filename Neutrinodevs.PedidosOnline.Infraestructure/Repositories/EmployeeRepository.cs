using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Employee;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        public EmployeeRepository(ND_PEDIDOS_ONLINEContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employess = (from emp in _context.Empleados
                             join users in _context.Usuarios on emp.UsuarioId equals users.IdUsuario
                             where emp.Estado == 1 && users.Estado == 1
                             select new Employee { 
                                IdEmpleado = emp.IdEmpleado,
                                EstadoActividad = emp.Estado,
                                Estado = emp.Estado,
                                FechaCreacion = emp.FechaCreacion,
                                FechaEliminacion = emp.FechaEliminacion,
                                Identificacion = emp.Identificacion,
                                TipoEmpleado = emp.TipoEmpleado,
                                UsuarioId = emp.UsuarioId
                             }).ToList();
            return employess;
        }
    }
}
