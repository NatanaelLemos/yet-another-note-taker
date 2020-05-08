using System;
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
    [ApiController]
    [Route("v0/users/{email}/notebooks")]
    public class NotebooksController : ControllerBase
    {
        private readonly INotebooksService _service;

        public NotebooksController(INotebooksService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<Hateoas<List<NotebookDto>>> GetAll(string email)
        {
            this.ValidateEmail(email);

            var notebooks = await _service.GetAll(email);
            return this.HateoasResult(notebooks);
        }

        [HttpGet("{notebookId}")]
        [Authorize]
        public async Task<Hateoas<NotebookDto>> Get(string email, Guid notebookId)
        {
            this.ValidateEmail(email);
            var notebook = await _service.Get(email, notebookId);
            return this.HateoasResult(notebook);
        }

        [HttpPost]
        [Authorize]
        public async Task<Hateoas<NotebookDto>> Post(string email, [FromBody]NotebookDto newDto)
        {
            this.ValidateEmail(email);

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            newDto = await _service.Add(email, newDto);
            return this.HateoasResult(newDto);
        }

        [HttpPut("{notebookId}")]
        [Authorize]
        public async Task<Hateoas<NotebookDto>> Put(string email, Guid notebookId, [FromBody]NotebookDto dto)
        {
            this.ValidateEmail(email);

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            dto = await _service.Update(email, notebookId, dto);
            return this.HateoasResult(dto);
        }

        [HttpDelete("{notebookId}")]
        [Authorize]
        public Task Delete(string email, Guid notebookId)
        {
            this.ValidateEmail(email);
            return _service.Delete(email, notebookId);
        }
    }
}
