using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        int Save(FullOrderDto order);
        FinalOrderDto Get(int idOrder);
        bool AssignDelivery(int idOrder, int idEmployee);
    }
}
