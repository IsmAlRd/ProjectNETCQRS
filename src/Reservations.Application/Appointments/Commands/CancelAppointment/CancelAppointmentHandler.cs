using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Appointments.Commands.CancelAppointment;

public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand>
{
  private readonly IAppointmentRepository _appointmentRepository;

  public CancelAppointmentHandler(IAppointmentRepository appointmentRepository)
  {
    _appointmentRepository = appointmentRepository;
  }

  public async Task Handle(CancelAppointmentCommand command, CancellationToken cancellationToken)
  {
    var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId, cancellationToken);

    if (appointment is null)
      throw new DomainException($"Reserva {command.AppointmentId} não encontrada.");

    appointment.Cancel();

    await _appointmentRepository.SaveChangesAsync(cancellationToken);
  }
}