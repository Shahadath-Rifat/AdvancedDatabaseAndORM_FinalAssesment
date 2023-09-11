using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdvancedDatabaseAndORM_FinalAssesment.Models;

namespace AdvancedDatabaseAndORM_FinalAssesment.Data
{
    public class AdvancedDatabaseAndORM_FinalAssesmentContext : DbContext
    {
        public AdvancedDatabaseAndORM_FinalAssesmentContext (DbContextOptions<AdvancedDatabaseAndORM_FinalAssesmentContext> options)
            : base(options)
        {
        }

        public DbSet<TodoList> TodoList { get; set; } = default!;

        public DbSet<TodoItem>? TodoItem { get; set; }
    }
}
