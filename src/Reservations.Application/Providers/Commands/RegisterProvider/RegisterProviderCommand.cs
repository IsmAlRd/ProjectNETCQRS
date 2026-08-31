using MediatR;

namespace Reservations.Application.Providers.Commands.RegisterProvider;

public record RegisterProviderCommand(
  string Name,
  string Email,
  string Specialty
) : IRequest<Guid>;