using Neutrinodevs.PedidosOnline.Domain.DTOs.Dashboard;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Neutrinodevs.PedidosOnline.Domain.Entities;
using Neutrinodevs.PedidosOnline.Domain.Models;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        int Save(FullOrderDto order);
        FinalOrderDto Get(int idOrder);
        bool AssignDelivery(int idOrder, int idEmployee);
        bool SetOrderStage(int idOrder, int idStage);
        bool FinishOrder(int idOrder, int idStage);
        OrderResumeDTO GetOrderResume(int idOrder);
        bool Cancel(CancelOrder cancel);
        int GetStage(int idOrder);
        ReportSales GetSalesReport(string startDate, string endDate);
    }
}
