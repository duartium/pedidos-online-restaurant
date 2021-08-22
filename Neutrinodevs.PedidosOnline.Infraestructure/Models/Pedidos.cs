﻿using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class Pedidos
    {
        public Pedidos()
        {
            PedidoDetalle = new HashSet<PedidoDetalle>();
        }

        public int IdPedido { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public int ClienteId { get; set; }

        public Clientes Cliente { get; set; }
        public ICollection<PedidoDetalle> PedidoDetalle { get; set; }
    }
}
