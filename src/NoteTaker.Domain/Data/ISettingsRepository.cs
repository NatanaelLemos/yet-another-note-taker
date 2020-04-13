using System;
using System.Threading.Tasks;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Data
{
    public interface ISettingsRepository
    {
        Task<Settings> GetByUserId(Guid userId);

        Task CreateOrUpdate(Settings settings);

        Task Save();
    }
}
