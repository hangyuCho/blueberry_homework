using BlueberryHomeworkApp.Domain.Entities;
using BlueberryHomeworkApp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BlueberryHomeworkApp.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PersonName> PersonNames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonNameConfiguration());
    }
}