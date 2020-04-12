using System.Threading.Tasks;
using NoteTaker.Client.Events.SettingsEvents;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Services
{
    public interface ISettingsAppService
    {
        void StartListeners();

        Task CreateOrUpdateSettingsCommandHandler(CreateOrUpdateSettingsCommand command);

        Task<Settings> SettingsQuery(SettingsQuery query);
    }
}
