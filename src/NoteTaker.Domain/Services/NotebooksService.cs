using System.Collections.Generic;
using NoteTaker.Domain.Data;

namespace NoteTaker.Domain.Services
{
    public interface INotebooksService
    {
        ICollection<Notebook> GetAll();

        void Create(Notebook notebook);
    }

    public class NotebooksRepository : INotebooksRepository
    {
        private static List<Notebook> _notebooks = new List<Notebook>();

        public void Create(Notebook notebook)
        {
            _notebooks.Add(notebook);
        }

        public void Delete(Notebook notebook)
        {
        }

        public ICollection<Notebook> GetAll()
        {
            return _notebooks;
        }

        public void Update(Notebook notebook)
        {
        }
    }

    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;

        public NotebooksService(INotebooksRepository repository)
        {
            _repository = repository;
        }

        public ICollection<Notebook> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(Notebook notebook)
        {
            _repository.Create(notebook);
        }

        public void Update(Notebook notebook)
        {
            _repository.Update(notebook);
        }

        public void Delete(Notebook notebook)
        {
            _repository.Delete(notebook);
        }
    }
}
