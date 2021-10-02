using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Presentation.Models
{
    public partial class Productos
    {
        public Productos()
        {
            PedidoDetalle = new HashSet<PedidoDetalle>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int? Tipo { get; set; }
        public int? Estado { get; set; }

        public ICollection<PedidoDetalle> PedidoDetalle { get; set; }
    }
}
