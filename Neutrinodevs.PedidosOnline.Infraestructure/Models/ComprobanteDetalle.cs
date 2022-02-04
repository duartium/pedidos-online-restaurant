using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class ComprobanteDetalle
    {
        public int IdComprobanteDet { get; set; }
        public int? ItemId { get; set; }
        public int ComprobanteId { get; set; }
        public int Estado { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
        public int? ProductId { get; set; }

        public ComprobanteVenta Comprobante { get; set; }
    }
}
