using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotebooksService : INotebooksService
    {
        private static List<NotebookDto> _allNotebooks = new List<NotebookDto>
            {
                new NotebookDto{ Key = "1", Name = "1" },
                new NotebookDto{ Key = "2", Name = "2" },
                new NotebookDto{ Key = "3", Name = "3" }
            };

        public Task<List<NotebookDto>> GetAll(string email)
        {
            return Task.FromResult(_allNotebooks);
        }

        public Task<NotebookDto> Create(string email, NotebookDto notebookDto)
        {
            notebookDto.Key = notebookDto.Name;
            _allNotebooks.Add(notebookDto);
            return Task.FromResult(notebookDto);
        }

        public Task<NotebookDto> Update(string email, NotebookDto notebookDto)
        {
            var dbItem = _allNotebooks.FirstOrDefault(n => n.Key == notebookDto.Key);
            if (dbItem != null)
            {
                _allNotebooks.Remove(dbItem);
            }

            _allNotebooks.Add(notebookDto);
            return Task.FromResult(notebookDto);
        }

        public Task Delete(string key)
        {
            var dbItem = _allNotebooks.FirstOrDefault(n => n.Key == key);
            if (dbItem != null)
            {
                _allNotebooks.Remove(dbItem);
            }
            return Task.CompletedTask;
        }
    }
}
