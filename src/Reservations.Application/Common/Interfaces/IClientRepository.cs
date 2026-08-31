using Reservations.Domain.Entities;

namespace Reservations.Application.Common.Interfaces;

public interface IClientRepository 
{
  Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task AddAsync(Client client, CancellationToken cancellationToken = default);
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
}