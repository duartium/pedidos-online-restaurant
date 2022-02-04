using Neutrinodevs.PedidosOnline.Domain.Contracts.Services;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using Neutrinodevs.PedidosOnline.Domain.Enums;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Services
{
    public class SaleService : ISaleService
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        public SaleService(ND_PEDIDOS_ONLINEContext context)
        {
            _context = context;
        }
        public bool Save(PosSaleDto sale)
        {
            int result = 0;
            using (_context.Database.BeginTransaction())
            {
                //se genera un nuevo secuancial
                var secuencialRow = _context.Secuenciales.Where(x => x.Nombre.Equals("factura"))
                    .Select(x => x).FirstOrDefault();

                int nuevoSecuencial = (int)secuencialRow.Secuencial + 1;
                secuencialRow.Secuencial = nuevoSecuencial;
                _context.Update(secuencialRow);
                _context.SaveChanges();

                var comprobante = new ComprobanteVenta
                {
                    Fecha = DateTime.Now.ToUniversalTime(),
                    Estado = (int)Estado.Activo,
                    Numero = nuevoSecuencial,
                    Subtotal = decimal.Parse(sale.Subtotal.Replace(",", ""), CultureInfo.InvariantCulture),
                    Iva = decimal.Parse(sale.Iva.Replace(",", ""), CultureInfo.InvariantCulture),
                    Total = decimal.Parse(sale.Total.Replace(",", ""), CultureInfo.InvariantCulture),
                    EsPos = true
                };
                _context.ComprobanteVenta.Add(comprobante);
                _context.SaveChanges();

                //se asigna los items del pedido al comprobante de venta
                var detallesComprobante = new List<ComprobanteDetalle>();
                foreach (var item in sale.Products)
                {
                    var detalle = new ComprobanteDetalle
                    {
                        ComprobanteId = comprobante.IdComprobante,
                        ItemId = null,
                        Estado = (int)Estado.Activo,
                        Cantidad = item.Quantity,
                        Precio = decimal.Parse(item.Price.Replace(",", ""), CultureInfo.InvariantCulture),
                        Total = decimal.Parse(item.full_value.Replace(",", ""), CultureInfo.InvariantCulture),
                        ProductId = item.Id
                    };
                    detallesComprobante.Add(detalle);
                }
                _context.ComprobanteDetalle.AddRange(detallesComprobante);
                result = _context.SaveChanges();

                _context.Database.CommitTransaction();
            }

            return (result > 0);
        }
    }
}
