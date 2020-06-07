using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly IUserState _userState;

        public SettingsService(ISettingsRepository repository, IUserState userState)
        {
            _repository = repository;
            _userState = userState;
        }

        public async Task<SettingsDto> Get()
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.Get(email, token);
        }

        public async Task<SettingsDto> Save(SettingsDto settings)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;

            return await _repository.Update(email, settings, token);
        }
    }
}
