using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class PedidoDetalle
    {
        public int IdPedidoDet { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

        public Pedidos Pedido { get; set; }
        public Productos Producto { get; set; }
    }
}
