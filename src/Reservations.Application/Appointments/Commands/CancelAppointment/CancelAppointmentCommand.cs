using MediatR;

namespace Reservations.Application.Appointments.Commands.CancelAppointment;

public record CancelAppointmentCommand(Guid AppointmentId) : IRequest;