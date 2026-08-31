using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Clients.Commands.RegisterClient;

public class RegisterClientHandler : IRequestHandler<RegisterClientCommand, Guid>
{
  private readonly IClientRepository _clientRepository;

  public RegisterClientHandler(IClientRepository clientRepository)
  {
    _clientRepository = clientRepository;
  }

  public async Task<Guid> Handle(RegisterClientCommand command, CancellationToken cancellationToken)
  {
    var emailAlreadyInUse = await _clientRepository.ExistsByEmailAsync(command.Email, cancellationToken);

    if (emailAlreadyInUse)
      throw new DomainException($"O e-mail '{command.Email}' já está cadastrado.");

    var client = new Client(command.Name, command.Email, command.Phone);

    await _clientRepository.AddAsync(client, cancellationToken);
    await _clientRepository.SaveChangesAsync(cancellationToken);

    return client.Id;
  }
}