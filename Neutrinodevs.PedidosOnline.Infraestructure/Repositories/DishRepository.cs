using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Dish;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Product;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class DishRepository : IDishRepository<ProductDTO>
    {
        private readonly ILogger<DishRepository> _logger;
        private readonly ND_PEDIDOS_ONLINEContext _context;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DishRepository(ILogger<DishRepository> logger, ND_PEDIDOS_ONLINEContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            try
            {
                var dishes = _context.Productos.Where(x => x.Estado == 1 && x.Tipo == 1)
                    .Select(x => new ProductDTO
                    {
                        Id = x.IdProducto,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Imagen = x.Imagen,
                        JsonProduct = JsonConvert.SerializeObject(new ProductDTO { 
                            Nombre = x.Nombre,
                            Precio = x.Precio,
                            Id = x.IdProducto
                        })
                    }).OrderByDescending(x => x.Id)
                    .ToList();
                return dishes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<ProductDTO> GetDrinks()
        {
            try
            {
                var drinks = _context.Productos.Where(x => x.Estado == 1 && x.Tipo == 2)
                     .Select(x => new ProductDTO
                     {
                         Id = x.IdProducto,
                         Nombre = x.Nombre,
                         Precio = x.Precio,
                         Imagen = x.Imagen
                     }).ToList();
                return drinks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public int Save(DishDto dishDto)
        {
            try
            {
                int resp = -1;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var _dish = new Productos {
                        Nombre = dishDto.Name,
                        Imagen = dishDto.ImageName,
                        Precio = dishDto.Price,
                        Tipo = dishDto.Type,
                        Estado = 1
                    };
                    _context.Productos.Add(_dish);
                    resp = _context.SaveChanges();

                    transaction.Commit();
                }
                return resp;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"db update error: {ex?.InnerException?.Message}");
                return -1;
            }
        }

    }
}
