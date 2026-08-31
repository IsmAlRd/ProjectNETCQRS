using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Persistence.Repositories;

public class ServiceTypeRepository : IServiceTypeRepository
{
  private readonly AppDbContext _context;
  public ServiceTypeRepository(AppDbContext context)
  {
    _context = context;
  }
  public async Task<IReadOnlyList<ServiceType>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _context.ServiceTypes
      .ToListAsync(cancellationToken);
  public async Task<ServiceType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _context.ServiceTypes
      .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
}