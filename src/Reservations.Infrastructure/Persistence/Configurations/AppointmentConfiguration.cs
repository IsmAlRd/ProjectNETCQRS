using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
  public void Configure(EntityTypeBuilder<Appointment> builder)
  {
    builder.HasKey(a => a.Id);

    builder.Property(a => a.ScheduledAt).IsRequired();
    builder.Property(a => a.EndsAt).IsRequired();

    builder.Property(a => a.Status)
      .IsRequired()
      .HasConversion<string>();

    builder.Property(a => a.Notes).HasMaxLength(500);

    builder.HasOne(a => a.Client)
      .WithMany()
      .HasForeignKey(a => a.ClientId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(a => a.Provider)
      .WithMany()
      .HasForeignKey(a => a.ProviderId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(a => a.ServiceType)
      .WithMany()
      .HasForeignKey(a => a.ServiceTypeId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}