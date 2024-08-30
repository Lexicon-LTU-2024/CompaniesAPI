using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Companies.API.Models.Entities;

namespace Companies.API.Data
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
    }

    
}
