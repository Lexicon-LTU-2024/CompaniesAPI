namespace Domain.Models.Exeptions;

public class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid id) : base($"The company with id: {id} was not found")
    {

    }
}


