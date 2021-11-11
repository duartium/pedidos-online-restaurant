namespace Neutrinodevs.PedidosOnline.Domain.Enums
{
    public enum EtapaPedido
    {
        PendienteAsignacion = 1,
        EnPreparacion = 2,
        EnCamino = 3,
        Entregado = 4,
        CanceladoPorCliente = 5,
        CanceladoPorRepartidor = 6
    }

    public enum TipoUsuario
    {
        Cliente = 1,
        Cajero = 2,
        Repartidor = 3,
        Administrador = 4
    }

    public enum TipoEmpleado
    {
        Repartidor = 1,
        Cajero = 2
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
