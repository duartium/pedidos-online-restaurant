using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System.Linq;
using System.Collections.Generic;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Newtonsoft.Json;
using System;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;

        public DeliveryRepository(ND_PEDIDOS_ONLINEContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderDeliveryDTO> GetAll()
        
        {
            List<OrderDeliveryDTO> orders = null;
            //TODO: Filtro de detalles con estado activo en pedido
            orders = _context.Pedidos.Include(cl => cl.Cliente)
                                           .Include(det => det.PedidoDetalle)
                                           //.ThenInclude(det => det.Where(x => x.Estado == 1))
                                           .Where(ped => ped.Estado == 1 && ped.DeliveryId == null
                                           && ped.PedidoDetalle.Any(o => o.Estado == 1))
                                           .Select(pedido => new OrderDeliveryDTO
                                           {
                                               IdOrder = pedido.IdPedido,
                                               Number = pedido.Numero.ToString(),
                                               Address = pedido.Cliente.Direccion,
                                               DeliveryTime = pedido.DeliveryTime,
                                               CustomerName = pedido.Cliente.Nombres + " " + pedido.Cliente.Apellidos,
                                               CellphoneNumber = pedido.Cliente.Telefono,
                                               Subtotal = decimal.Parse(pedido.Subtotal.ToString(), CultureInfo.InvariantCulture),
                                               Total = decimal.Parse(pedido.Total.ToString(), CultureInfo.InvariantCulture),
                                               JsonProducts = JsonConvert.SerializeObject(pedido.PedidoDetalle)
                                           })
                                           .ToList();
            return orders;                         
        }

        public IEnumerable<OrderDeliveryDTO> GetDeliveriesByDealer(int idDealer)
        {
            List<OrderDeliveryDTO> orders = null;
            orders = _context.Pedidos.Where(x => x.Estado == 1)
                                           .Include(cl => cl.Cliente)
                                           .Include(det => det.PedidoDetalle)
                                           .Where(ped => ped.Estado == 1 && ped.DeliveryId == idDealer
                                           && ped.PedidoDetalle.Any(o => o.Estado == 1))
                                           .Select(pedido => new OrderDeliveryDTO
                                           {
                                               IdOrder = pedido.IdPedido,
                                               IdStage = (int)pedido.Stage,
                                               Number = pedido.Numero.ToString(),
                                               Address = pedido.Cliente.Direccion,
                                               DeliveryTime = pedido.DeliveryTime,
                                               CustomerName = pedido.Cliente.Nombres + " " + pedido.Cliente.Apellidos,
                                               CellphoneNumber = pedido.Cliente.Telefono,
                                               Subtotal = decimal.Parse(pedido.Subtotal.ToString(), CultureInfo.InvariantCulture),
                                               Total = decimal.Parse(pedido.Total.ToString(), CultureInfo.InvariantCulture),
                                               JsonProducts = JsonConvert.SerializeObject(pedido.PedidoDetalle)
                                           }).ToList();
            return orders;
        }
    }
}
