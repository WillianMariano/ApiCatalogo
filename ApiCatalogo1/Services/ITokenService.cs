using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalogo1.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
        string GenerateRefrashToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
