using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
using System;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using System.Globalization;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        public CustomerRepository(ND_PEDIDOS_ONLINEContext context)
        {
            _context = context;
        }

        public CustomerDTO Get(string identification)
        {
            return _context.Clientes.Where(x => x.Identificacion.Equals(identification.Trim()) && x.Estado == 1)
                .Select(x => new CustomerDTO { 
                    IdClient = x.IdCliente,
                    FullName = x.Nombres.ToUpper() + " " + x.Apellidos.ToUpper(),
                    Identification = identification
                }).FirstOrDefault();
        }

        public bool Save(PosSaleDto sale)
        {
            int result = 0;
            

            return (result > 0);
        }

    }
}
