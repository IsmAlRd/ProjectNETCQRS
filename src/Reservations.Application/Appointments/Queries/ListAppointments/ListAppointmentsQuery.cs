using MediatR;

namespace Reservations.Application.Appointments.Queries.ListAppointments;

public record ListAppointmentsQuery(
  Guid? ClientId,
  Guid? ProviderId
) : IRequest<IReadOnlyList<AppointmentSummaryDto>>;