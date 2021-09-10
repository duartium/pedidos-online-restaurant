using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Linq;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class UserRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ND_PEDIDOS_ONLINEContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Save(UserDto userDto)
        {
            try
            {
                int resp = -1;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var _user = new Usuarios
                    {
                        Nombres = userDto.FirstName + " " + userDto.LastName,
                        Email = userDto.Email,
                        Estado = 1,
                        TipoUsuario = 1,
                        Password = userDto.Password
                    };
                    _context.Usuarios.Add(_user);
                    _context.SaveChanges();

                    var _cliente = new Clientes
                    {
                        Identificacion = userDto.Identification,
                        Nombres = userDto.FirstName,
                        Apellidos = userDto.LastName,
                        Direccion = userDto.Address,
                        Email = userDto.Email,
                        Telefono = String.Empty,
                        TipoCliente = 1,
                        Estado = 1,
                        UsuarioId = _user.IdUsuario
                    };
                    _context.Clientes.Add(_cliente);
                    _context.SaveChanges();
                    resp = _cliente.IdCliente;

                    transaction.Commit();
                }
                return resp > 0 ? true : false; 
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"db update error: {ex?.InnerException?.Message}");
                return false;
            }
        }

        public bool ValidateDuplicateUser(string identification)
        {
            try
            {
                int count = -1;
                count = (from client in _context.Clientes
                            where client.Estado == 1
                            && client.Identificacion == identification
                            select client).Count();
                return (count <= 0) ? false : true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

    }
}
