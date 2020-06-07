using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Services
{
    public interface ISettingsService
    {
        Task<SettingsDto> Get(string email);
        Task<SettingsDto> Update(string email, SettingsDto settings);
    }
}
