using Microsoft.Extensions.Logging;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
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
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DishRepository(ILogger<DishRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            _logger.LogInformation("GetAll Dishes");
            try
            {
                using (var bd = new ND_PEDIDOS_ONLINEContext())
                {
                    var dishes = bd.Productos.Where(x => x.Estado == 1 && x.Tipo == 1)
                        .Select(x => new ProductDTO
                        {
                            Nombre = x.Nombre,
                            Precio = x.Precio,
                            Imagen = x.Imagen,
                            JsonProduct = JsonConvert.SerializeObject(new ProductDTO { 
                                Nombre = x.Nombre,
                                Precio = x.Precio,
                                Id = x.IdProducto
                            })
                        }).ToList();
                    return dishes;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
