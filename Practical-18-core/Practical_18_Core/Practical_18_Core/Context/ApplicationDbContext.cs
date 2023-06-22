using Microsoft.EntityFrameworkCore;
using Practical_18_Core.Models;
using System.Collections.Generic;

namespace Practical_18_Core.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; } = null!;
    }
}
