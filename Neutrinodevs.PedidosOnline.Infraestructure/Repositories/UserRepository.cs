using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.DTOs.User;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class UserRepository
    {
        public UserRepository()
        {

        }

        public bool Save(UserDto userDto)
        {
            try
            {
                int resp = -1;
                using (var bd = new ND_PEDIDOS_ONLINEContext())
                {
                    using (var transaction = bd.Database.BeginTransaction())
                    {
                        var _user = new Usuarios
                        {
                            Nombres = userDto.FirstName + " " + userDto.LastName,
                            Email = userDto.Email,
                            Estado = 1,
                            TipoUsuario = 1,
                            Password = userDto.Password
                        };
                        bd.Usuarios.Add(_user);
                        bd.SaveChanges();

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
                        bd.Clientes.Add(_cliente);
                        bd.SaveChanges();
                        resp = _cliente.IdCliente;

                        transaction.Commit();
                    }
                }
                return resp > 0 ? true : false; 
            }
            catch (DbUpdateException exc)
            {
                //Logger.LogError(exc, $"{nameof(SaveChanges)} db update error: {exc?.InnerException?.Message}");
                return false;
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entidad de tipo \"{0}\" en estado \"{1}\" tiene los siguientes errores de validación:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //            Utils.WriteLog("RoadSafetyRepository>Save", $"- Propiedad: {ve.PropertyName}, Error: {ve.ErrorMessage}");
            //    }
            //    return -1;
            //}
        }

    }
}
