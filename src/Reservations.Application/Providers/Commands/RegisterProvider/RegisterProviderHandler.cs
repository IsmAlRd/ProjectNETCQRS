using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Providers.Commands.RegisterProvider;

public class RegisterProviderHandler : IRequestHandler<RegisterProviderCommand, Guid>
{
  private readonly IProviderRepository _providerRepository;

  public RegisterProviderHandler(IProviderRepository providerRepository)
  {
    _providerRepository = providerRepository;
  }

  public async Task<Guid> Handle(RegisterProviderCommand command, CancellationToken cancellationToken)
  {
    var emailAlreadyInUse = await _providerRepository.ExistsByEmailAsync(command.Email, cancellationToken);

    if (emailAlreadyInUse)
      throw new DomainException($"O e-mail '{command.Email}' já está cadastrado.");

    var provider = new Provider(command.Name, command.Email, command.Specialty);

    await _providerRepository.AddAsync(provider, cancellationToken);
    await _providerRepository.SaveChangesAsync(cancellationToken);

    return provider.Id;
  }
}