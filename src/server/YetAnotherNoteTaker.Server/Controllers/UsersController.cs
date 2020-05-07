using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLemos.Api.Framework.Exceptions;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Controllers
{
    /// <summary>
    /// <see cref="UserDto"/>'s controllers.
    /// </summary>
    [ApiController]
    [Route("v0/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _service;

        public UsersController(ILogger<UsersController> logger, IUsersService service)
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
        ///     POST /v0/users
        ///     {
        ///         "email": "my@email.com",
        ///         "password": "my-password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created user.</response>
        /// <response code="422">If the user is invalid.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public Task<UserDto> Post([FromBody] NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            return _service.CreateUser(newUser);
        }
    }
}
