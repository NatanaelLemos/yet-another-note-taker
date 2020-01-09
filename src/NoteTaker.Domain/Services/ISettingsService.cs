﻿using System.Threading.Tasks;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public interface ISettingsService
    {
        Task<Settings> Get();

        Task CreateOrUpdateSettings(Settings settings);
    }
}