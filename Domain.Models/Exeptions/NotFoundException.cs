using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Exeptions;
public abstract class NotFoundException : Exception
{
    public string Title { get; }
    protected NotFoundException(string message, string title = "Not Found") : base(message)
    {
        Title = title;
    }

}
