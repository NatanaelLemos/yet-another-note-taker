using System.Threading.Tasks;
using NoteTaker.Client.Events.SettingsEvents;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Services
{
    public interface ISettingsAppService
    {
        void StartListeners();

        Task CreateOrUpdateSettingsCommandHandler(CreateOrUpdateSettingsCommand command);

        Task<SettingsDto> SettingsQuery(SettingsQuery query);
    }
}
