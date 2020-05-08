using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Security
{
    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersService _usersService;

        public PasswordValidator(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var result = await _usersService.ValidatePassword(context.UserName, context.Password);

            if (result)
            {
                context.Result = new GrantValidationResult(context.UserName, "password", null, "local", null);
            }
            else
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "The username and password do not match",
                    null);
            }
        }
    }

    /*
     Example of authentication:
     POST http://localhost:5000/connect/token

     x-www-form-urlencoded
     {
        grant_type: password,
        username: user@example.com,
        password: string,
        scope: YetAnotherNoteTaker,
        client_id: notetaker,
        client_secret: notetaker
     }

     */
}
