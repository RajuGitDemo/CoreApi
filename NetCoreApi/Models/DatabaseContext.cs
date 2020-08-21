using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
    }
}
