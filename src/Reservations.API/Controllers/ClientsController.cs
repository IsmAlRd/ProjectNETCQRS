using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.Clients.Commands.RegisterClient;

namespace Reservations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
  private readonly IMediator _mediator;

  public ClientsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> Register(RegisterClientCommand command, CancellationToken cancellationToken)
  {
    var id = await _mediator.Send(command, cancellationToken);
    return CreatedAtAction(nameof(Register), new { id }, null);
  }
}