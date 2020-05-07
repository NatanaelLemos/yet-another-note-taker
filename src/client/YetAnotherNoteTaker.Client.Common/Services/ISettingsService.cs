﻿using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface ISettingsService
    {
        Task<SettingsDto> Get(Guid userId);

        Task Save(Guid userId, SettingsDto settings);
    }
}