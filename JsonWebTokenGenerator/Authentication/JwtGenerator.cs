using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JsonWebTokenGenerator.Services;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebTokenGenerator.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IDateTimeProvider dateTimeProvider, JwtSettings jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(string username, IEnumerable<Claim>? claims)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        });

        if (claims != null)
        {
            claimsIdentity.AddClaims(claims);
        }

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials,
            claims: claimsIdentity.Claims
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}