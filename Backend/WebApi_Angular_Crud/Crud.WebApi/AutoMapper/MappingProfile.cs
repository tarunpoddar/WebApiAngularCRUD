using AutoMapper;
using Crud.WebApi.DTOs;
using Crud.WebApi.Models;

namespace Crud.WebApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping logic
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(emp => emp.Id, op => op.MapFrom(e => e.Id))
                .ForMember(emp => emp.EmployeeName, op => op.MapFrom(e => e.Name))
                .ForMember(emp => emp.EmployeePhone, op => op.MapFrom(e => e.Phone))
                .ForMember(emp => emp.EmployeeDateOfBirth, op => op.MapFrom(e => e.DateOfBirth))
                .ForMember(emp => emp.EmployeeEmail, op => op.MapFrom(e => e.Email))
                .ReverseMap();

        }
    }
}
