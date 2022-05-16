using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        CustomerDTO Get(string identification);
        IEnumerable<CustomerDTO> GetAll();
        bool Save(CustomerSaveDTO userDto);
    }
}
