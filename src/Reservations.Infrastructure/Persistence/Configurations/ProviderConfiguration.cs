using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
  public void Configure(EntityTypeBuilder<Provider> builder)
  {
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Name)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(p => p.Email)
      .IsRequired()
      .HasMaxLength(150);

    builder.Property(p => p.Specialty)
      .IsRequired()
      .HasMaxLength(100);

    builder.HasIndex(p => p.Email).IsUnique();
  }
}