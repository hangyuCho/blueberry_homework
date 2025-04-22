using BlueberryHomeworkApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueberryHomeworkApp.Infrastructure.Configurations;

public class PersonNameConfiguration: IEntityTypeConfiguration<PersonName>
{
    public void Configure(EntityTypeBuilder<PersonName> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}