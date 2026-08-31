using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain.Entities;

namespace Reservations.Infrastructure.Authentication;

public class JwtService : IJwtService
{
  private readonly IConfiguration _configuration;

  public JwtService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateToken(Provider provider)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, provider.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Email, provider.Email),
      new Claim(ClaimTypes.Role, "Provider")
    };

    var key = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
    
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _configuration["Jwt:Issuer"],
      audience: _configuration["Jwt:Audience"],
      claims: claims,
      expires: DateTime.UtcNow.AddHours(1),
      signingCredentials: credentials);
      
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}