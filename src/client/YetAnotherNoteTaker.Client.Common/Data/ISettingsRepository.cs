using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Data
{
    public interface ISettingsRepository
    {
        Task<SettingsDto> Get(string email, string token);
        Task<SettingsDto> Update(string email, SettingsDto settings, string token);
    }
}
