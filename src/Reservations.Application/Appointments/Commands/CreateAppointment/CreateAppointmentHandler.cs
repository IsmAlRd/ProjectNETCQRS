using MediatR;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;
using Reservations.Domain.Exceptions;

namespace Reservations.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
  private readonly IAppointmentRepository _appointmentRepository;
  private readonly IClientRepository _clientRepository;
  private readonly IProviderRepository _providerRepository;
  private readonly IServiceTypeRepository _serviceTypeRepository;

  public CreateAppointmentHandler(
    IAppointmentRepository appointmentRepository,
    IClientRepository clientRepository,
    IProviderRepository providerRepository,
    IServiceTypeRepository serviceTypeRepository)
  {
    _appointmentRepository = appointmentRepository;
    _clientRepository = clientRepository;
    _providerRepository = providerRepository;
    _serviceTypeRepository = serviceTypeRepository;
  }

  public async Task<Guid> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken = default)
  {
    var client = await _clientRepository.GetByIdAsync(command.ClientId, cancellationToken);
    if (client is null)
      throw new DomainException($"Cliente {command.ClientId} não encontrado.");

    var provider = await _providerRepository.GetByIdAsync(command.ProviderId, cancellationToken);
    if (provider is null)
      throw new DomainException($"Prestador {command.ProviderId} não encontrado.");

    var serviceType = await _serviceTypeRepository.GetByIdAsync(command.ServiceTypeId, cancellationToken);
    if (serviceType is null)
      throw new DomainException($"Tipo de serviço {command.ServiceTypeId} não encontrado.");

    var hasConflict = await _appointmentRepository.HasConflictAsync(command.ProviderId, command.ScheduledAt, command.EndsAt, cancellationToken: cancellationToken);

    if (hasConflict)
      throw new DomainException("O prestador já possui uma reserva neste horário.");

    var appointment = new Appointment(
      command.ClientId,
      command.ProviderId,
      command.ServiceTypeId,
      command.ScheduledAt,
      command.EndsAt,
      command.Notes);

    await _appointmentRepository.AddAsync(appointment, cancellationToken);
    await _appointmentRepository.SaveChangesAsync(cancellationToken);

    return appointment.Id;
  }
}
