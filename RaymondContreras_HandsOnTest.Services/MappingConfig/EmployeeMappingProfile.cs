using AutoMapper;
using RaymondContreras_HandsOnTest.Models;
using RaymondContreras_HandsOnTest.ViewModels;

namespace RaymondContreras_HandsOnTest.Services.MappingConfig
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.HourlySalary, opt => opt.MapFrom(src => src.HourlySalary))
                .ForMember(dto => dto.MonthlySalary, opt => opt.MapFrom(src => src.MonthlySalary))
                .ForMember(dto => dto.ContractTypeName, opt => opt.MapFrom(src => src.ContractTypeName))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dto => dto.RoleDescription, opt => opt.MapFrom(src => src.RoleDescription))
                .ForMember(dto => dto.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(src => src.RoleName));
        }
    }
}
