using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
  private readonly AppDbContext _context;

  public ClientRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _context.Clients
        .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
  
  public async Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    => await _context.Clients
      .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);

  public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    => await _context.Clients
      .AnyAsync(c => c.Email == email, cancellationToken);

  public async Task AddAsync(Client client, CancellationToken cancellationToken = default)
    => await _context.Clients.AddAsync(client, cancellationToken);

  public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
  => await _context.SaveChangesAsync(cancellationToken);
}