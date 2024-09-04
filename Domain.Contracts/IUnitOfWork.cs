namespace Domain.Contracts;
public interface IUnitOfWork
{
    ICompanyRepository Company { get; }
    IEmployeeRepository Employee { get; }

    Task CompleteAsync();
}