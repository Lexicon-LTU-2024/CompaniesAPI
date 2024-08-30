using AutoMapper;

namespace Companies.API.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyCreateDto, Company>();

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
