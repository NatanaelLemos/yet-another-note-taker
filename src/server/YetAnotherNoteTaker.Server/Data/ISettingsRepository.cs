using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public interface ISettingsRepository
    {
        Task<Settings> Get(string email);
        Task<Settings> Add(Settings settings);
        Task<Settings> Update(Settings settings);
    }
}
