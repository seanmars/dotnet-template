using System.Security.Claims;

namespace JsonWebTokenGenerator.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(string username, IEnumerable<Claim>? claims);
}