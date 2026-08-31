using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
  private readonly IAppointmentRepository _appointmentRepository;

  public GetAppointmentByIdHandler(IAppointmentRepository appointmentRepository)
  {
    _appointmentRepository = appointmentRepository;
  }

  public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken)
  {
    var appointment = await _appointmentRepository.GetByIdAsync(query.AppointmentId, cancellationToken);

    if (appointment is null)
      throw new DomainException($"Reserva {query.AppointmentId} não encontrada.");

    return new AppointmentDto(
      appointment.Id,
      appointment.ClientId,
      appointment.ProviderId,
      appointment.ServiceTypeId,
      appointment.ScheduledAt,
      appointment.EndsAt,
      appointment.Status.ToString(),
      appointment.Notes);
  }
}