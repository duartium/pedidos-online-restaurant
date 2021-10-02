using AutoMapper;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Employee;
using Neutrinodevs.PedidosOnline.Domain.Entities;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.IdEmployee, dto => dto.MapFrom(source => source.IdEmpleado))
                .ForMember(dest => dest.Identification, dto => dto.MapFrom(source => source.Identificacion))
                .ForMember(dest => dest.FullName, dto => dto.MapFrom(source => source.Nombres + " " + source.Apellidos));
        }
    }
}
