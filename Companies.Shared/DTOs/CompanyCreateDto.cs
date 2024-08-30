﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Shared.DTOs
{
    public record CompanyCreateDto : CompanyManipulationDto
    {
        public IEnumerable<EmployeeDto>? Employees { get; init; }
    }
}
