using MediatR;

namespace Reservations.Application.Appointments.Commands.RescheduleAppointment;

public record RescheduleAppointmentCommand(
  Guid AppointmentId,
  DateTime NewScheduledAt,
  DateTime NewEndsAt
) : IRequest;