using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        int Save(FullOrderDto order);
        FullOrderDto Get(int idOrder);
    }
}
