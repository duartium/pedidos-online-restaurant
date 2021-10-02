using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Presentation.Models
{
    public partial class ComprobanteDetalle
    {
        public int IdComprobanteDet { get; set; }
        public int ItemId { get; set; }
        public int ComprobanteId { get; set; }
        public int Estado { get; set; }

        public ComprobanteVenta Comprobante { get; set; }
    }
}
