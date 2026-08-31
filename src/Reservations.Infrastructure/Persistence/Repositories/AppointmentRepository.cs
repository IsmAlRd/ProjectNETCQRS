using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;
using Reservations.Domain.Enums;

namespace Reservations.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
  private readonly AppDbContext _context;

  public AppointmentRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _context.Appointments
        .Include(a => a.Client)
        .Include(a => a.Provider)
        .Include(a => a.ServiceType)
        .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

  public async Task<IReadOnlyList<Appointment>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default)
    => await _context.Appointments
      .Where(a => a.ClientId == clientId)
      .ToListAsync(cancellationToken);

  public async Task<IReadOnlyList<Appointment>> GetByProviderIdAsync(Guid providerId, CancellationToken cancellationToken = default)
    => await _context.Appointments
      .Where(a => a.ProviderId == providerId)
      .ToListAsync(cancellationToken);

  public async Task<bool> HasConflictAsync(Guid providerId, DateTime scheduledAt, DateTime endsAt, Guid? excludeAppointmentId = null, CancellationToken cancellationToken = default)
    => await _context.Appointments
      .Where(a => a.ProviderId == providerId
      && a.Id != excludeAppointmentId
      && a.Status != AppointmentStatus.Canceled
      && a.ScheduledAt < endsAt
      && a.EndsAt > scheduledAt)
      .AnyAsync(cancellationToken);
      
  public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default) 
    => await _context.Appointments.AddAsync(appointment, cancellationToken);

  public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    => await _context.SaveChangesAsync(cancellationToken);
}