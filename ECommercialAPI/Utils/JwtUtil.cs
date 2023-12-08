using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommercialAPI.Constants;
using Microsoft.IdentityModel.Tokens;

namespace ECommercialAPI.Utils;

public static class JwtUtil
{
    private static readonly IConfiguration Configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables(EnvironmentVariableConstant.Prefix).Build();

    public static string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[JwtConstant.SecretKey]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Email, username)
        };
        var currentDateTime = DateTimeUtil.ConvertToVietNamDateTime(DateTime.Now);
        var token = new JwtSecurityToken(Configuration[JwtConstant.Issuer], Configuration[JwtConstant.Audience], claims,
            notBefore: currentDateTime,
            expires: currentDateTime.AddDays(1),
            credentials);
        return tokenHandler.WriteToken(token);
    }
    
}