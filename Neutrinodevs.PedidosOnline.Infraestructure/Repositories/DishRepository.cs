using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Product;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class DishRepository : IDishRepository<ProductDTO>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            try
            {
                using (var bd = new ND_PEDIDOS_ONLINEContext())
                {
                    var dishes = bd.Productos.Where(x => x.Estado == 1 && x.Tipo == 1)
                        .Select(x => new ProductDTO
                        { 
                           Nombre = x.Nombre,
                           Precio = x.Precio,
                           Imagen = x.Imagen
                        }).ToList();
                    return dishes;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
