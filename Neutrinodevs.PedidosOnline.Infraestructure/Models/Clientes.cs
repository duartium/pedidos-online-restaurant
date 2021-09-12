using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class Clientes
    {
        public Clientes()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        public int IdCliente { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int? TipoCliente { get; set; }
        public int? Estado { get; set; }
        public int? UsuarioId { get; set; }
        public string CodigoVerificacion { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public int? Etapa { get; set; }

        public Usuarios Usuario { get; set; }
        public ICollection<Pedidos> Pedidos { get; set; }
    }
}
