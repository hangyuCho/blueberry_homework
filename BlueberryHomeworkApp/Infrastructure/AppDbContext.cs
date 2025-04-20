using BlueberryHomeworkApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueberryHomeworkApp.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PersonName> PersonNames { get; set; }
}