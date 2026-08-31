namespace Reservations.Application.Appointments.Queries.GetAppointmentById;

public record AppointmentDto(
  Guid Id,
  Guid ClientId,
  Guid ProviderId,
  Guid ServiceTypeId,
  DateTime ScheduledAt,
  DateTime EndsAt,
  string Status,
  string? Notes
);