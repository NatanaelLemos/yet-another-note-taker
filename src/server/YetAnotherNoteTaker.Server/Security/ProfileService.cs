using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Security
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersService _usersService;

        public ProfileService(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var email = context?.Subject?.Claims?.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var user = await _usersService.GetUserByEmail(email);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Email),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
            };

            context.IssuedClaims = claims;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //TODO: Check if user is activated
            return Task.FromResult(true);
        }
    }
}
