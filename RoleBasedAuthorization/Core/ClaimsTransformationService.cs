using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace RoleBasedAuthorization.Core
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        private readonly UserService userService;

        public ClaimsTransformationService(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
            {
                return principal;
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var roles = await userService.UserRoles(userId);

            if (roles.Count == 0)
            {
                return principal;
            }

            foreach (var role in roles)
            {
                if (principal.HasClaim(ClaimTypes.Role, role))
                {
                    continue;
                }
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return principal;
        }
    }
}
