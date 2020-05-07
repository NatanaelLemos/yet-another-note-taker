using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Services;

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
        private readonly IUsersService _service;

        public RegisterController(ILogger<RegisterController> logger, IUsersService service)
        {
            _logger = logger;
            _service = service;
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
            return _service.CreateUser(newUser);
        }
    }
}
