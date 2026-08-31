using MediatR;

namespace Reservations.Application.Clients.Commands.RegisterClient;

public record RegisterClientCommand(
  string Name,
  string Email,
  string Phone
) : IRequest<Guid>;