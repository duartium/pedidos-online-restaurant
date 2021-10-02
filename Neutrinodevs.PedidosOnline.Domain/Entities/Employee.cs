using System;

namespace Neutrinodevs.PedidosOnline.Domain.Entities
{
    public class Employee
    {
        public int IdEmpleado { get; set; }
        public string Identificacion { get; set; }
        public int TipoEmpleado { get; set; }
        public int? EstadoActividad { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioId { get; set; }
        public string Usuario { get; set; }
    }
}
