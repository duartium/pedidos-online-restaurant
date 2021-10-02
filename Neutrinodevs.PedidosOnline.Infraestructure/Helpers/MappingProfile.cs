using AutoMapper;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Employee;
using Neutrinodevs.PedidosOnline.Domain.Entities;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
