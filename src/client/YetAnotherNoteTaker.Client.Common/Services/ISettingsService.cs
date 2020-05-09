using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface ISettingsService
    {
        Task<SettingsDto> Get(string email);

        Task Save(string email, SettingsDto settings);
    }
}
