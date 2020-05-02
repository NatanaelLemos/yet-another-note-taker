using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotebooksService : INotebooksService
    {
        private static List<NotebookDto> _allNotebooks = new List<NotebookDto>
            {
                new NotebookDto{ Id = Guid.NewGuid(), Name = "1" },
                new NotebookDto{ Id = Guid.NewGuid(), Name = "2" },
                new NotebookDto{ Id = Guid.NewGuid(), Name = "3" }
            };

        public Task<List<NotebookDto>> GetAll(Guid userId)
        {
            return Task.FromResult(_allNotebooks);
        }

        public Task<NotebookDto> Create(Guid userId, NotebookDto notebookDto)
        {
            notebookDto.Id = Guid.NewGuid();
            _allNotebooks.Add(notebookDto);
            return Task.FromResult(notebookDto);
        }

        public Task<NotebookDto> Update(Guid userId, NotebookDto notebookDto)
        {
            var dbItem = _allNotebooks.FirstOrDefault(n => n.Id == notebookDto.Id);
            if (dbItem != null)
            {
                _allNotebooks.Remove(dbItem);
            }

            _allNotebooks.Add(notebookDto);
            return Task.FromResult(notebookDto);
        }

        public Task Delete(Guid id)
        {
            var dbItem = _allNotebooks.FirstOrDefault(n => n.Id == id);
            if (dbItem != null)
            {
                _allNotebooks.Remove(dbItem);
            }
            return Task.CompletedTask;
        }
    }
}
