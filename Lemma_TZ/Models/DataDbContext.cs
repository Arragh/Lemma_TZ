using Microsoft.EntityFrameworkCore;
using System;

namespace Lemma_TZ.Models
{
    public class DataDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) => Database.EnsureCreated();
    }
}
