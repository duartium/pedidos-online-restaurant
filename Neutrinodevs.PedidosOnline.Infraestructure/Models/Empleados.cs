﻿using System;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class Empleados
    {
        public int IdEmpleado { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int TipoEmpleado { get; set; }
        public int? EstadoActividad { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioId { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
