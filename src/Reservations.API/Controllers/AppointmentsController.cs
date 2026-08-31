using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.Appointments.Commands.CancelAppointment;
using Reservations.Application.Appointments.Commands.CreateAppointment;
using Reservations.Application.Appointments.Commands.RescheduleAppointment;
using Reservations.Application.Appointments.Queries.GetAppointmentById;
using Reservations.Application.Appointments.Queries.ListAppointments;

namespace Reservations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
  private readonly IMediator _mediator;

  public AppointmentsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateAppointmentCommand command, CancellationToken cancellationToken)
  {
    var id = await _mediator.Send(command, cancellationToken);
    return CreatedAtAction(nameof(GetById), new { id }, null);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
  {
    var appointment = await _mediator.Send(new GetAppointmentByIdQuery(id), cancellationToken);
    return Ok(appointment);
  }

  [HttpGet]
  public async Task<IActionResult> List([FromQuery] Guid? clientId, [FromQuery] Guid? providerId, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new ListAppointmentsQuery(clientId, providerId), cancellationToken);
    return Ok(result);
  }

  [HttpPut("{id:guid}/cancel")]
  public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
  {
    await _mediator.Send(new CancelAppointmentCommand(id), cancellationToken);
    return NoContent();
  }

  [HttpPut("{id:guid}/reschedule")]
  public async Task<IActionResult> Reschedule(Guid id, RescheduleAppointmentCommand command, CancellationToken cancellationToken)
  {
    await _mediator.Send(command with { AppointmentId = id }, cancellationToken);
    return NoContent();
  }
}