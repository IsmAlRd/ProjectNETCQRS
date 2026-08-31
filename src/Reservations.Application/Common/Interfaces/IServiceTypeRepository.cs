using Reservations.Domain.Entities;

namespace Reservations.Application.Common.Interfaces;

public interface IServiceTypeRepository
{
  Task<ServiceType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<ServiceType>> GetAllAsync(CancellationToken cancellationToken = default);
}