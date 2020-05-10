using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Exceptions;
using NLemos.Api.Framework.Extensions;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Services
{
    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;
        private readonly INotesRepository _notesRepository;

        public NotebooksService(INotebooksRepository repository, INotesRepository notesRepository)
        {
            _repository = repository;
            _notesRepository = notesRepository;
        }

        public async Task<List<NotebookDto>> GetAll(string userEmail)
        {
            var result = await _repository.GetAll(userEmail);
            return result.Select(r => new NotebookDto
            {
                Key = r.Key,
                Name = r.Name
            }).ToList();
        }

        public async Task<NotebookDto> Get(string email, string notebookKey)
        {
            var result = await _repository.Get(email, notebookKey);
            if (result == null)
            {
                return null;
            }

            return new NotebookDto
            {
                Key = result.Key,
                Name = result.Name
            };
        }

        public async Task<NotebookDto> Add(string userEmail, NotebookDto notebook)
        {
            var key = notebook.Name.GenerateKey();
            var current = await _repository.Get(userEmail, key);
            if (current != null)
            {
                throw new InvalidParametersException(nameof(notebook.Name), "There's already a notebook with this name.");
            }

            current = await _repository.Add(new Notebook
            {
                Key = key,
                Name = notebook.Name,
                UserEmail = userEmail
            });

            return new NotebookDto
            {
                Key = current.Key,
                Name = current.Name
            };
        }

        public async Task<NotebookDto> Update(string userEmail, string notebookKey, NotebookDto notebook)
        {
            var current = await _repository.Get(userEmail, notebookKey);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(notebookKey), "Notebook not found.");
            }

            current.Key = notebook.Name.GenerateKey();
            current.Name = notebook.Name;
            current = await _repository.Update(current);

            await _notesRepository.UpdateNotebookKeys(notebookKey, current.Key);

            return new NotebookDto
            {
                Key = current.Key,
                Name = current.Name
            };
        }

        public async Task Delete(string userEmail, string notebookKey)
        {
            var current = await _repository.Get(userEmail, notebookKey);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(notebookKey), "Notebook not found.");
            }

            var deleteTask = _repository.Delete(current);
            var deleteNotesTask = _notesRepository.DeleteByNotebookKey(userEmail, notebookKey);

            await Task.WhenAll(deleteTask, deleteNotesTask);
        }
    }
}
