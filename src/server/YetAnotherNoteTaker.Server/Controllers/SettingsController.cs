using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLemos.Api.Framework.Extensions.Controllers;
using NLemos.Api.Framework.Models;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v0/users/{email}/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;

        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<SettingsDto>> Get(string email)
        {
            this.ValidateEmail(email);
            var settings = await _service.Get(email);
            return this.HateoasResult(settings);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<SettingsDto>> Put(string email, [FromBody] SettingsDto settings)
        {
            this.ValidateEmail(email);
            settings = await _service.Update(email, settings);
            return this.HateoasResult(settings);
        }
    }
}
