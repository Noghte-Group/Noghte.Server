using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Noghte.Domain;
using Noghte.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Noghte.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var secretKeyFromAppSetting = _configuration.GetValue<string>("JwtSettings:SecretKey");
        var issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
        var audience = _configuration.GetValue<string>("JwtSettings:Audience");
        var notBeforeMinutes = _configuration.GetValue<string>("JwtSettings:NotBeforeMinutes");
        var expirationMinutes = _configuration.GetValue<string>("JwtSettings:ExpirationMinutes");


        var secretKey = Encoding.UTF8.GetBytes(secretKeyFromAppSetting); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);


        var claims = await GetClaimsAsync(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(Convert.ToInt32(notBeforeMinutes)),
            Expires = DateTime.Now.AddMinutes(Convert.ToInt32(expirationMinutes)),
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(claims)
        };


        var tokenHanlder = new JwtSecurityTokenHandler();

        var securityToken = tokenHanlder.CreateToken(descriptor);

        return tokenHanlder.WriteToken(securityToken);
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var list = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("phone_number", user.PhoneNumber),
            new Claim(ClaimTypes.Role, "Author")
        };

        return list;
    }

}
