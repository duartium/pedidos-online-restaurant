namespace Neutrinodevs.PedidosOnline.Domain.Enums
{
    public enum EtapaPedido
    {
        Registro = 1
    }

    public enum TipoUsuario
    {
        Cliente = 1,
        Cajero = 2,
        Repartidor = 3,
        Administrador = 4
    }

    public enum EstadoEmpleado
    {
        Disponible = 1,
        EnProcesoEntrega = 2
    }

    public enum Estado
    {
        Activo = 1,
        Inactivo = 0
    }
}
