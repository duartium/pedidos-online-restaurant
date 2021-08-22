using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class Mesas
    {
        public int IdMesa { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
    }
}
