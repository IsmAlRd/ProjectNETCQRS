using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Repositories;

public class ProviderRepository : IProviderRepository
{ 
  private readonly AppDbContext _context;

  public ProviderRepository(AppDbContext context)
  {
    _context = context;
  }
  public async Task AddAsync(Provider provider, CancellationToken cancellationToken = default)
    => await _context.Providers.AddAsync(provider, cancellationToken);

  public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    => await _context.Providers
      .AnyAsync(p => p.Email == email, cancellationToken);

  public async Task<Provider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _context.Providers
      .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

  public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    => await _context.SaveChangesAsync(cancellationToken);
}