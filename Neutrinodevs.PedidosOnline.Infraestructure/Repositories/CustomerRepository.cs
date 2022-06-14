using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;
using Neutrinodevs.PedidosOnline.Infraestructure.Extensions.Common;

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
                    Names = x.Nombres,
                    Identification = identification,
                    Email = x.Email,
                    Phone = x.Telefono,
                    Surnames = x.Apellidos
                }).FirstOrDefault();
        }

        public CustomerDTO GetById(string identification)
        {
            return _context.Clientes.Where(x => x.Identificacion.Equals(identification.Trim()) && x.Estado == 1)
                .Select(x => new CustomerDTO
                {
                    IdClient = x.IdCliente,
                    FullName = x.Nombres.ToUpper() + " " + x.Apellidos.ToUpper(),
                    Identification = identification
                }).FirstOrDefault();
        }

        public bool Update(CustomerUpdateDto customer)
        {
            try
            {
                int resp = -1;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var client = _context.Clientes.Where(x => x.Estado == 1 && x.IdCliente == customer.IdClient).First();
                    client.Identificacion = customer.Identification;
                    client.Nombres = customer.Names;
                    client.Apellidos = customer.Surnames;
                    client.Direccion = customer.Address;
                    client.Email = customer.Email;
                    client.Telefono = customer.Phone;
                    _context.Entry(client).State = EntityState.Modified;
                    resp = _context.SaveChanges();
                    
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


        public IEnumerable<CustomerDTO> GetAll()
        {
            return _context.Clientes.Where(x => x.Estado == 1)
                .Select(x => new CustomerDTO { 
                    IdClient = x.IdCliente,
                    Email = x.Email,
                    Identification = x.Identificacion,
                    Phone = x.Telefono,
                    FullName = x.GetFullNames()
                }).ToList();
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

        public CustomerDTO GetById(int id)
        {
            return _context.Clientes.Where(x => x.Estado == 1 && x.IdCliente == id)
                .Include(x => x.Usuario)
                .Select(x => new CustomerDTO
                {
                    IdClient = x.IdCliente,
                    Email = x.Email,
                    Identification = x.Identificacion,
                    Phone = x.Telefono,
                    Names = x.Nombres,
                    Surnames = x.Apellidos,
                    Address = x.Direccion,
                    Username = x.Usuario.Username
                }).FirstOrDefault();
        }
    }
}
