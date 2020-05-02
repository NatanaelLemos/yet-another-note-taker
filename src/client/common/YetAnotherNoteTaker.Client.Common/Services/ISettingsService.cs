using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface ISettingsService
    {
        Task<SettingsDto> Get(Guid userId);

        Task Save(Guid userId, SettingsDto settings);
    }
}
