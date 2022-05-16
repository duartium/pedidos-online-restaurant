using Neutrinodevs.PedidosOnline.Infraestructure.Models;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Extensions.Common
{
    public static partial class CommonExtensions
    {
        /// <summary>
        /// Retorna los nombre completos del cliente
        /// </summary>
        /// <param name="clientes"></param>
        /// <returns></returns>
        public static string GetFullNames(this Clientes clientes)
        {
            return $"{clientes.Nombres} {clientes.Apellidos}";
        }
    }
}
