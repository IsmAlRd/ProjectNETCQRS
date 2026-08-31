using MediatR;

namespace Reservations.Application.Appointments.Queries.GetAppointmentById;

public record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<AppointmentDto>;