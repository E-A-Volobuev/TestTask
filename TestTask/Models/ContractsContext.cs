using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class ContractsContext:DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public ContractsContext(DbContextOptions<ContractsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
