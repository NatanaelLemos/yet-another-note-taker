using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Data
{
    public class XamarinSettingsRepository : ISettingsRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public XamarinSettingsRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public Task<SettingsDto> Get(string email, string token)
        {
            var url = _urlBuilder.Settings.Get(email);
            return _restClient.Get<SettingsDto>(url, token);
        }

        public Task<SettingsDto> Update(string email, SettingsDto settings, string token)
        {
            var url = _urlBuilder.Settings.Put(email);
            return _restClient.Put<SettingsDto>(url, settings, token);
        }
    }
}
