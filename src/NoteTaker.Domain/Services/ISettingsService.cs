using System;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface ISettingsService
    {
        Task<SettingsDto> GetByUserId(Guid userId);

        Task CreateOrUpdateSettings(Guid userId, SettingsDto settings);
    }
}
