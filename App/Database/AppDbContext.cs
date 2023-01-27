using App.Features.Assignments.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Base.Database;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AssignmentModel> Assignments { get; set; }
}