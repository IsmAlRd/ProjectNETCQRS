using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Providers.Commands.RegisterProvider;

namespace Reservations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvidersController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IProviderRepository _providerRepository;
  private readonly IJwtService _jwtService;

  public ProvidersController(IMediator mediator, IProviderRepository providerRepository, IJwtService jwtService)
  {
    _mediator = mediator;
    _providerRepository = providerRepository;
    _jwtService = jwtService;
  }

  [HttpPost]
  public async Task<IActionResult> Register(RegisterProviderCommand command, CancellationToken cancellationToken)
  {
    var id = await _mediator.Send(command, cancellationToken);
    return CreatedAtAction(nameof(Register), new { id }, null);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
  {
    var provider = await _providerRepository.GetByIdAsync(request.ProviderId, cancellationToken);
    if (provider is null)
      return NotFound("Prestador não encontrado.");

    var token = _jwtService.GenerateToken(provider);
    return Ok(new { token });
  }
}

public record LoginRequest(Guid ProviderId);