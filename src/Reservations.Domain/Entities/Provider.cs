using Reservations.Domain.Common;

namespace Reservations.Domain.Entities;

public class Provider : Entity
{
  public string Name { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string Specialty { get; private set; } = string.Empty;

  private Provider() { }

  public Provider(string name, string email, string specialty)
  {
    Name = name;
    Email = email;
    Specialty = specialty;
  }

  public void UpdateSpecialty(string specialty)
  {
    Specialty = specialty;
  }
  public void UpdateEmail(string email)
  {
    Email = email;
  }
}