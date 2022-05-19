using Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IDeliveryRepository : IRepositoryBase<OrderDeliveryDTO>
    {
        IEnumerable<OrderDeliveryDTO> GetDeliveriesByDealer(int idDealer);
        DailyOrdersDelivery GetDailySales(int idDealer);
        IEnumerable<DealerDTO> GetDealers();
    }
}
