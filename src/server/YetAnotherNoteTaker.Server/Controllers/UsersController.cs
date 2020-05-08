using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLemos.Api.Framework.Exceptions;
using NLemos.Api.Framework.Extensions.Controllers;
using NLemos.Api.Framework.Models;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Controllers
{
    /// <summary>
    /// <see cref="UserDto"/>'s controllers.
    /// </summary>
    [ApiController]
    [Route("v0/users")]
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
        /// Returns a user.
        /// </summary>
        /// <param name="email">Users email.</param>
        /// <returns>Instance of user.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /v0/users/user@example.com
        ///
        /// </remarks>
        /// <response code="200">Returns the user.</response>
        /// <response code="401">If user is unauthorized.</response>
        /// <response code="422">If the email is invalid.</response>
        [HttpGet("{email}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<UserDto>> Get(string email)
        {
            this.ValidateEmail(email);

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            var user = await _service.GetUserByEmail(email);
            return this.HateoasResult(user);
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
        ///         "email": "user@example.com",
        ///         "password": "my-password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created user.</response>
        /// <response code="422">If the user is invalid.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<UserDto>> Post([FromBody] NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            var user = await _service.CreateUser(newUser);
            return this.HateoasResult(user);
        }

        /// <summary>
        /// Updates an user.
        /// </summary>
        /// <param name="email">The user's email for sanity check.</param>
        /// <param name="newUser">The updated user body.</param>
        /// <returns>Instance of the updated user.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /v0/users/user@email.com
        ///     {
        ///         "email": "newemail@email.com",
        ///         "password": "my-password-2"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the updated user.</response>
        /// <response code="422">If the user is invalid.</response>
        [HttpPut("{email}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<UserDto>> Put(string email, [FromBody] NewUserDto newUser)
        {
            this.ValidateEmail(email);

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            var user = await _service.UpdateUser(email, newUser);
            return this.HateoasResult(user);
        }
    }
}
