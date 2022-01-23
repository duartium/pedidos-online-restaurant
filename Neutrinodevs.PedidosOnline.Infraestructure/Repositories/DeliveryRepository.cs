using Microsoft.EntityFrameworkCore;
using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
                                           }).OrderByDescending(x => x.IdOrder)
                                           .ToList();
            return orders;                         
        }

        public DailyOrdersDelivery GetDailySales(int idDealer)
        {
            DailyOrdersDelivery dailySales = new DailyOrdersDelivery();
            dailySales.Items = _context.Pedidos.Where(x => x.Estado == 1 && x.Stage == (int)EtapaPedido.Entregado)
                                           .Where(ped => ped.Estado == 1 && ped.DeliveryId == idDealer 
                                           && ped.Fecha.ToString("yyyy-MM-dd").Equals(DateTime.Today.ToString("yyyy-MM-dd")))
                                           .Select(pedido => new ItemDailyOrderDelivery
                                           {
                                               Number = pedido.Numero.ToString().PadLeft(7, '0'),
                                               Time = pedido.DeliveryTime,
                                               Total = decimal.Parse(pedido.Total.ToString(), CultureInfo.InvariantCulture)
                                           }).ToList();

            dailySales.Total = Math.Round(dailySales.Items.Select(x => x.Total).Sum(), 2);
            dailySales.TotalProducts = 0;
            dailySales.SuccessfulDeliveries = dailySales.Items.Count;
            return dailySales;
        }

        public IEnumerable<OrderDeliveryDTO> GetDeliveriesByDealer(int idDealer)
        {
            List<OrderDeliveryDTO> orders = null;
            orders = _context.Pedidos.Where(x => x.Estado == 1 && x.Stage >= 2 && x.Stage <= 3)
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
