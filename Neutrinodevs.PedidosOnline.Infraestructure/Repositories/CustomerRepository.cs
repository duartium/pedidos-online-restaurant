using Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Infraestructure.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;
using System;
using Neutrinodevs.PedidosOnline.Domain.Enums;
using System.Globalization;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ND_PEDIDOS_ONLINEContext _context;
        public CustomerRepository(ND_PEDIDOS_ONLINEContext context)
        {
            _context = context;
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
                };
                _context.ComprobanteVenta.Add(comprobante);
                _context.SaveChanges();

                //se asigna los items del pedido al comprobante de venta
                var detallesComprobante = new List<ComprobanteDetalle>();
                foreach (var idItem in sale.ProductIds)
                {
                    var detalle = new ComprobanteDetalle
                    {
                        ComprobanteId = comprobante.IdComprobante,
                        ItemId = idItem.Id,
                        Estado = (int)Estado.Activo
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
