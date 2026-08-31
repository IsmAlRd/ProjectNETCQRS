using Microsoft.EntityFrameworkCore;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Appointment> Appointments => Set<Appointment>();
  public DbSet<Client> Clients => Set<Client>();
  public DbSet<Provider> Providers => Set<Provider>();
  public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}