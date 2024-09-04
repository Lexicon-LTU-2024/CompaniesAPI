using AutoMapper;
using Domain.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        _mapper = mapper;
    }
}
