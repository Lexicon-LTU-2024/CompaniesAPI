using AutoMapper;

namespace Companies.API.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Address, opt =>
                opt.MapFrom(src => $"{src.Address}{(string.IsNullOrEmpty(src.Country) ? string.Empty : ", ")}{src.Country}"));
              
            CreateMap<CompanyCreateDto, Company>();

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
