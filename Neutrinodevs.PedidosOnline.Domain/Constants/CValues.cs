namespace Neutrinodevs.PedidosOnline.Domain.Constants
{
    public static class CValues
    {
        public static readonly string[] Stages = {
            "Pendiente de asignación",
            "En preparación",
            "En camino",
            "Entregado",
            "Cancelado por cliente",
            "Cancelado por repartidor"
        };

        public static readonly string[] Roles = {
            "Cliente",
            "Cajero",
            "Repartidor",
            "Administrador"
        };
    }
}
