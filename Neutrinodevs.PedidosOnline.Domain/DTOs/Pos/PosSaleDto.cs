using Neutrinodevs.PedidosOnline.Domain.Models;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Pos
{
    public class PosSaleDto
    {
        public Customer Customer { get; set; }
        public int[] ProductIds { get; set; }
    }
}
