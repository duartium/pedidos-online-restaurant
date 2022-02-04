using Neutrinodevs.PedidosOnline.Domain.DTOs.Pos;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Services
{
    public interface ISaleService
    {
        bool Save(PosSaleDto sale);
    }
}
