using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.Entities
{
    public class OrderDetailDelivery
    {
        public int IdPedidoDet { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

    }
}
