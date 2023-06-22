using Microsoft.EntityFrameworkCore;
using Practical_18_API.Entities;

namespace Practical_18_API.Context;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
	public DbSet<Student> Students { get; set; } = null!;
}
