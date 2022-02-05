using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System.Linq;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(ND_PEDIDOS_ONLINEContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
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

        public bool Save(CustomerSaveDTO userDto)
        {
            try
            {
                int resp = -1;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var _cliente = new Clientes
                    {
                        Identificacion = userDto.Identification,
                        Nombres = userDto.FirstName,
                        Apellidos = userDto.LastName,
                        Direccion = null,
                        Email = userDto.Email,
                        Telefono = userDto.Contact,
                        TipoCliente = 2,
                        CodigoVerificacion = null,
                        Estado = 1,
                        Etapa = (int)EtapaPedido.Entregado,
                        UsuarioId = null
                    };
                    _context.Clientes.Add(_cliente);
                    _context.SaveChanges();
                    resp = _cliente.IdCliente;

                    transaction.Commit();
                }
                return resp > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"db update error: {ex?.InnerException?.Message}");
                return false;
            }
        }

    }
}
