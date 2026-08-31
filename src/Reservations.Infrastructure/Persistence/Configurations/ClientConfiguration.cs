using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
  public void Configure(EntityTypeBuilder<Client> builder)
  {
    builder.HasKey(c => c.Id);

    builder.Property(c => c.Name)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(c => c.Email)
      .IsRequired()
      .HasMaxLength(150);

    builder.Property(c => c.Phone)
      .IsRequired()
      .HasMaxLength(20);

    builder.HasIndex(c => c.Email).IsUnique();
  }
}