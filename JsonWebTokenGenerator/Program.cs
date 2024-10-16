using System.Security.Cryptography;
using System.Text.Json;
using JsonWebTokenGenerator.Authentication;
using JsonWebTokenGenerator.Services;

var key = new byte[32];
RandomNumberGenerator.Create().GetBytes(key);
var base64Secret = Convert.ToBase64String(key);

var dateTimeProvider = new DateTimeProvider();
var jwtSetting = new JwtSettings()
{
    Issuer = "dev",
    Audience = "dev",
    ExpiryMinutes = 60,
    Secret = base64Secret
};

var jwtGenerator = new JwtGenerator(dateTimeProvider, jwtSetting);

var jwt = jwtGenerator.GenerateToken("dev", null);
Console.WriteLine(JsonSerializer.Serialize(jwtSetting));
Console.WriteLine(jwt);