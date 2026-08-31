using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
  public void Configure(EntityTypeBuilder<ServiceType> builder)
  {
    builder.HasKey(s => s.Id);

    builder.Property(s => s.Name)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(s => s.DurationMinutes)
      .IsRequired();

    builder.Property(s => s.Description)
      .HasMaxLength(300);
  }
}