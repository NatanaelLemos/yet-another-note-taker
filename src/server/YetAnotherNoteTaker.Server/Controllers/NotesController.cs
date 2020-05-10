using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLemos.Api.Framework.Exceptions;
using NLemos.Api.Framework.Extensions.Controllers;
using NLemos.Api.Framework.Models;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v0/users/{email}/notebooks")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _service;

        public NotesController(INotesService service)
        {
            _service = service;
        }

        [HttpGet("notes")]
        public async Task<Hateoas<List<NoteDto>>> GetAll(string email)
        {
            var result = await _service.GetAll(email);
            return this.HateoasResult(result);
        }

        [HttpGet("{notebookKey}/notes")]
        public async Task<Hateoas<List<NoteDto>>> GetByNotebookKey(string email, string notebookKey)
        {
            var result = await _service.GetByNotebookKey(email, notebookKey);
            return this.HateoasResult(result);
        }

        [HttpGet("{notebookKey}/notes/{noteKey}")]
        public async Task<Hateoas<NoteDto>> Get(string email, string notebookKey, string noteKey)
        {
            var result = await _service.Get(email, notebookKey, noteKey);
            return this.HateoasResult(result);
        }

        [HttpPost("{notebookKey}/notes")]
        public async Task<Hateoas<NoteDto>> Post(string email, string notebookKey, [FromBody] NoteDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            var result = await _service.Add(email, notebookKey, dto);
            return this.HateoasResult(result);
        }

        [HttpPut("{notebookKey}/notes/{noteKey}")]
        public async Task<Hateoas<NoteDto>> Put(string email, string notebookKey, string noteKey, [FromBody] NoteDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            var result = await _service.Update(email, notebookKey, noteKey, dto);
            return this.HateoasResult(result);
        }

        [HttpDelete("{notebookKey}/notes/{noteKey}")]
        public async Task Delete(string email, string notebookKey, string noteKey)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            await _service.Delete(email, notebookKey, noteKey);
        }
    }
}
