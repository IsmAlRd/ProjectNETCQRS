using Reservations.Domain.Entities;

namespace Reservations.Application.Common.Interfaces;

public interface IJwtService
{
  string GenerateToken(Provider provider);
}