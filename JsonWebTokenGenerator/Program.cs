// See https://aka.ms/new-console-template for more information

using JsonWebTokenGenerator.Authentication;
using JsonWebTokenGenerator.Services;

var dateTimeProvider = new DateTimeProvider();
var jwtSetting = new JwtSettings()
{
    Issuer = "dev",
    Audience = "dev",
    ExpiryMinutes = 60,
    Secret = "secret-key-json-web-token-generator"
};

var jwtGenerator = new JwtGenerator(dateTimeProvider, jwtSetting);

var jwt = jwtGenerator.GenerateToken("dev", null);
Console.WriteLine(jwt);