using MediatR;
using Reservations.Application.Common.Interfaces;

namespace Reservations.Application.Appointments.Queries.ListAppointments;

public class ListAppointmentsHandler : IRequestHandler<ListAppointmentsQuery, IReadOnlyList<AppointmentSummaryDto>>
{
  private readonly IAppointmentRepository _appointmentRepository;

  public ListAppointmentsHandler(IAppointmentRepository appointmentRepository)
  {
    _appointmentRepository = appointmentRepository;
  }

  public async Task<IReadOnlyList<AppointmentSummaryDto>> Handle(ListAppointmentsQuery query, CancellationToken cancellationToken)
  {
    var appointments = query switch
    {
      { ClientId: not null }   => await _appointmentRepository.GetByClientIdAsync(query.ClientId.Value, cancellationToken),
      { ProviderId: not null } => await _appointmentRepository.GetByProviderIdAsync(query.ProviderId.Value, cancellationToken),  
    _                          => []
    };

    return appointments
      .Select(a => new AppointmentSummaryDto(
        a.Id,
        a.ClientId,
        a.ProviderId,
        a.ScheduledAt,
        a.EndsAt,
        a.Status.ToString()))
      .ToList();
      
  }
}