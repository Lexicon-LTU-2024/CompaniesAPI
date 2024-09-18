using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Shared.Request;
public abstract class RequestParams
{
    private int pageSize = 2;
    const int maxPageSize = 20;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > maxPageSize ? maxPageSize : value;
    }
}

public class CompanyRequestParams : RequestParams
{
    
}
