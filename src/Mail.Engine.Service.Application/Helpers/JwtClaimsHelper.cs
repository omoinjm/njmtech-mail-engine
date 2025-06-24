using System.IdentityModel.Tokens.Jwt;

namespace Mail.Engine.Service.Application.Helpers
{
    public class JwtClaimsHelper
    {
        public static bool IsJwtTokenExpired(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(jwtToken)) return true;

            var token = handler.ReadJwtToken(jwtToken);
            var expClaim = token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (long.TryParse(expClaim, out var exp))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                return expirationTime < DateTime.UtcNow;
            }

            return true; // Treat as expired if no exp or invalid
        }
    }
}
