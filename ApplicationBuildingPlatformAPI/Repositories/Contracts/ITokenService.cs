using ApplicationBuildingPlatformAPI.Models.DTO;
using System.Security.Claims;

namespace ApplicationBuildingPlatformAPI.Repositories.Contracts
{
    public interface ITokenService
    {
        TokenResponse GetToken(IEnumerable<Claim> claim);
        //TokenResponse GetToken(string token);
        //TokenResponse GetToken(string token, Claim claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
