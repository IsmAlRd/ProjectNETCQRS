using Reservations.Domain.Entities;

namespace Reservations.Application.Common.Interfaces;

public interface IProviderRepository
{
  Task<Provider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task AddAsync(Provider provider, CancellationToken cancellationToken = default);
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
}