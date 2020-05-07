using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Controllers
{
    /// <summary>
    /// Controller utilized to register new users.
    /// </summary>
    [ApiController]
    [Route("v0/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new instance of <see cref="NewUserDto"/>.
        /// </summary>
        /// <param name="newUser">The new user body.</param>
        /// <returns>Instance of the user created.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /v0/register
        ///     {
        ///         "email": "my@email.com",
        ///         "password": "my-password"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public Task<UserDto> Post([FromBody] NewUserDto newUser)
        {
            return Task.FromResult(new UserDto { Id = Guid.NewGuid(), Email = newUser.Email });
        }
    }
}
