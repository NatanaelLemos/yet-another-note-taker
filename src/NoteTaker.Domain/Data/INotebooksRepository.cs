﻿using System.Collections.Generic;

namespace NoteTaker.Domain.Data
{
    public interface INotebooksRepository
    {
        void Create(Notebook notebook);

        void Update(Notebook notebook);

        void Delete(Notebook notebook);

        ICollection<Notebook> GetAll();
    }
}
