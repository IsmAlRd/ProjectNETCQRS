using Reservations.Domain.Common;

namespace Reservations.Domain.Entities;

public class ServiceType : Entity
{
  public string Name { get; private set; } = string.Empty;
  public int DurationMinutes { get; private set; }
  public string? Description { get; private set; }

  private ServiceType() { }

  public ServiceType(string name, int durationMinutes, string? description = null)
  {
    Name = name;
    DurationMinutes = durationMinutes;
    Description = description;
  }
}