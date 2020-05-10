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
    /// <summary>
    /// <see cref="NotebookDto"/>'s controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("v0/users/{email}/notebooks")]
    public class NotebooksController : ControllerBase
    {
        private readonly INotebooksService _service;

        public NotebooksController(INotebooksService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all notebooks of a user.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <returns>List of notebooks.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /v0/users/user@example.com/notebooks
        ///
        /// </remarks>
        /// <response code="200">Returns the list of notebooks.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<List<NotebookDto>>> GetAll(string email)
        {
            this.ValidateEmail(email);

            var notebooks = await _service.GetAll(email);
            return this.HateoasResult(notebooks);
        }

        /// <summary>
        /// Gets one notebook.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="notebookKey">Notebook's key.</param>
        /// <returns>The notebook.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /v0/users/user@example.com/notebooks/exampleNotebook
        ///
        /// </remarks>
        /// <response code="200">Returns the notebook.</response>
        [HttpGet("{notebookKey}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<NotebookDto>> Get(string email, string notebookKey)
        {
            this.ValidateEmail(email);
            var notebook = await _service.Get(email, notebookKey);
            return this.HateoasResult(notebook);
        }

        /// <summary>
        /// Posts a new notebook.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="newDto">A new notebook.</param>
        /// <returns>The newly created notebook.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /v0/users/user@example.com/notebooks
        ///     {
        ///         "name": "example notebook"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
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

        /// <summary>
        /// Puts an updated notebook.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="notebookKey">Notebook key.</param>
        /// <param name="dto">The updated notebook.</param>
        /// <returns>The updated notebook.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /v0/users/user@example.com/notebooks/examplenotebook
        ///     {
        ///         "name": "new name"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{notebookKey}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<Hateoas<NotebookDto>> Put(string email, string notebookKey, [FromBody]NotebookDto dto)
        {
            this.ValidateEmail(email);

            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            dto = await _service.Update(email, notebookKey, dto);
            return this.HateoasResult(dto);
        }

        /// <summary>
        /// Deletes a notebook.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="notebookKey">Notebook key.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /v0/users/user@example.com/notebooks/examplenotebook
        ///
        /// </remarks>
        [HttpDelete("{notebookKey}")]
        public Task Delete(string email, string notebookKey)
        {
            this.ValidateEmail(email);
            return _service.Delete(email, notebookKey);
        }
    }
}
