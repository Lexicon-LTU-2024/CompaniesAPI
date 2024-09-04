using Domain.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;
public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork uow;

    public CompanyService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}
