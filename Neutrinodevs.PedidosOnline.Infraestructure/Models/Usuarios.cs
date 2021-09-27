using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Clientes = new HashSet<Clientes>();
        }

        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TipoUsuario { get; set; }
        public int Estado { get; set; }

        public ICollection<Clientes> Clientes { get; set; }
    }
}
