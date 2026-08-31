using Reservations.Domain.Entities;

namespace Reservations.Application.Common.Interfaces;

public interface IAppointmentRepository
{
  Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<Appointment>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<Appointment>> GetByProviderIdAsync(Guid providerId, CancellationToken cancellationToken = default);
  Task<bool> HasConflictAsync(Guid providerId, DateTime scheduledAt, DateTime endsAt, Guid? excludeAppointmentId = null, CancellationToken cancellationToken = default);
  Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
}