namespace Domain.Models.Exeptions;


    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid id) : base($"The employee with id: {id} was not found")
        {

        }
    }


