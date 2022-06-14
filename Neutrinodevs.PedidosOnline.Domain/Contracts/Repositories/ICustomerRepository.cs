using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        CustomerDTO Get(string identification);
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO GetById(int id);
        bool Save(CustomerSaveDTO userDto);
        bool Update(CustomerUpdateDto customer);

    }
}
