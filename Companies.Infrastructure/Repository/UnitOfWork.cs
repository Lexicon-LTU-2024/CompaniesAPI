using Companies.Infrastructure.Data;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly DBContext _db;

    public ICompanyRepository CompanyRepository { get; }
    public UnitOfWork(DBContext db)
    {
        CompanyRepository = new CompanyRepository(db);
        _db = db;
    }

    public async Task CompleteAsync()
    {
        await _db.SaveChangesAsync();
    }
}
