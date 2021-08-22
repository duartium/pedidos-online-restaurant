using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class ComprobanteVenta
    {
        public ComprobanteVenta()
        {
            ComprobanteDetalle = new HashSet<ComprobanteDetalle>();
        }

        public int IdComprobante { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }

        public ICollection<ComprobanteDetalle> ComprobanteDetalle { get; set; }
    }
}
