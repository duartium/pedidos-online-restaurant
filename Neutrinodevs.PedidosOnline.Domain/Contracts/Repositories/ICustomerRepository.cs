using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        CustomerDTO Get(string identification);
        bool Save(CustomerSaveDTO userDto);
    }
}
