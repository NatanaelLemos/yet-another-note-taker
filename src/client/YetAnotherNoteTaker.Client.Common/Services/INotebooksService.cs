﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotebooksService
    {
        Task<List<NotebookDto>> GetAll(Guid userId);

        Task<NotebookDto> Create(Guid userId, NotebookDto notebookDto);

        Task<NotebookDto> Update(Guid userId, NotebookDto notebookDto);

        Task Delete(Guid id);
    }
}