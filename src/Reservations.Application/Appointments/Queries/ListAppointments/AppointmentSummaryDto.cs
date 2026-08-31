namespace Reservations.Application.Appointments.Queries.ListAppointments;

public record AppointmentSummaryDto(
  Guid Id,
  Guid ClientId,
  Guid ProviderId,
  DateTime ScheduledAt,
  DateTime EndsAt,
  string Status
);