using Reservations.Domain.Common;

namespace Reservations.Domain.Entities;

public class Client : Entity
{
  public string Name { get; private set; } = string.Empty;
  public string Email { get; private set; } = string.Empty;
  public string Phone { get; private set; } = string.Empty;

  private Client() { }

  public Client(string name, string email, string phone)
  {
    Name = name;
    Email = email;
    Phone = phone;
  }

  public void UpdateContact(string email, string phone)
  {
    Email = email;
    Phone = phone;
  }
}