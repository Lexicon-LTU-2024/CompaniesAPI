using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Data
{
    public class DBContext : IdentityDbContext<Employee, IdentityRole, string>
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies => Set<Company>();
       // public DbSet<Employee> Employee { get; set; } = default!;
    }


}
