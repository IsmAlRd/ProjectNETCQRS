using Reservations.Domain.Common;
using Reservations.Domain.Enums;
using Reservations.Domain.Exceptions;

namespace Reservations.Domain.Entities;

public class Appointment : Entity
{
  public Guid ClientId { get; private set; }
  public Guid ProviderId { get; private set; }
  public Guid ServiceTypeId { get; private set;}
  public DateTime ScheduledAt { get; private set; }
  public DateTime EndsAt { get; private set; }
  public AppointmentStatus Status { get; private set; }
  public string? Notes { get; private set; }

  public Client Client { get; private set; } = null!;
  public Provider Provider { get; private set; } = null!;
  public ServiceType ServiceType { get; private set; } = null!;

  private Appointment() { }

  public Appointment(Guid clientId, Guid providerId, Guid serviceTypeId,
  DateTime scheduledAt, DateTime endsAt, string? notes = null)
  {
    ClientId = clientId;
    ProviderId = providerId;
    ServiceTypeId = serviceTypeId;
    ScheduledAt = scheduledAt;
    EndsAt = endsAt;
    Status = AppointmentStatus.Scheduled;
    Notes = notes;
  }

  public void Cancel()
  {
    if (Status == AppointmentStatus.Canceled)
      throw new DomainException("A reserva já está cancelada.");

    if (Status == AppointmentStatus.Completed)
      throw new DomainException("Não é possível cancelar uma reserva concluída.");

    Status = AppointmentStatus.Canceled;
  }

  public void Confirm()
  {
    if (Status != AppointmentStatus.Scheduled)
      throw new DomainException("Somente reservas agendadas podem ser confirmadas.");

    Status = AppointmentStatus.Confirmed;
  }

  public void Complete()
  {
    if (Status != AppointmentStatus.Confirmed)
      throw new DomainException("Somente reservas confirmadas podem ser concluídas.");

    Status = AppointmentStatus.Completed;
  }

  public void Reschedule(DateTime newScheduledAt, DateTime newEndsAt)
  {
    if (Status == AppointmentStatus.Canceled)
      throw new DomainException("Não é possível reagendar uma reserva cancelada.");
    
    if (Status == AppointmentStatus.Completed)
      throw new DomainException("Não é possível reagendar uma reserva concluída.");

    if (newScheduledAt <= DateTime.UtcNow)
      throw new DomainException("A nova data deve ser futura.");

    ScheduledAt = newScheduledAt;
    EndsAt = newEndsAt;
    Status = AppointmentStatus.Scheduled;
  }
}