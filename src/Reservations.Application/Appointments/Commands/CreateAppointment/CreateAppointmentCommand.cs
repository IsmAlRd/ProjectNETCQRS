using MediatR;

namespace Reservations.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(
  Guid ClientId,
  Guid ProviderId,
  Guid ServiceTypeId,
  DateTime ScheduledAt,
  DateTime EndsAt,
  string? Notes
) : IRequest<Guid>;