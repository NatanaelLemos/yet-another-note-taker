﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public interface INotebooksRepository
    {
        Task<List<Notebook>> GetAll(string userEmail);
        Task<Notebook> Get(string userEmail, Guid id);
        Task<Notebook> GetByName(string userEmail, string name);
        Task<Notebook> Add(Notebook notebook);
        Task<Notebook> Update(Notebook notebook);
        Task Delete(Notebook notebook);
    }
}
