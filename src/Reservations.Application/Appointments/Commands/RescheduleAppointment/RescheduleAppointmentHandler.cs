using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Appointments.Commands.RescheduleAppointment;

public class RescheduleAppointmentHandler : IRequestHandler<RescheduleAppointmentCommand>
{
  private readonly IAppointmentRepository _appointmentRepository;

  public RescheduleAppointmentHandler(IAppointmentRepository appointmentRepository)
  {
    _appointmentRepository = appointmentRepository;
  }

  public async Task Handle(RescheduleAppointmentCommand command, CancellationToken cancellationToken)
  {
    var appointment = await _appointmentRepository.GetByIdAsync(command.AppointmentId, cancellationToken);

    if (appointment is null)
      throw new DomainException($"Reserva {command.AppointmentId} não encontrada.");

    var hasConflict = await _appointmentRepository.HasConflictAsync(
      appointment.ProviderId,
      command.NewScheduledAt,
      command.NewEndsAt,
      excludeAppointmentId: command.AppointmentId,
      cancellationToken: cancellationToken);

    if (hasConflict)
      throw new DomainException("O prestador já possui uma reserva neste horário.");

    appointment.Reschedule(command.NewScheduledAt, command.NewEndsAt);

    await _appointmentRepository.SaveChangesAsync(cancellationToken);

  }
}