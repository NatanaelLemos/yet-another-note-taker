using System.Threading.Tasks;

namespace YetAnotherNoteTaker.Client.Common.Http
{
    public interface IRestClient
    {
        Task<T> Get<T>(string url, string authToken = "");

        Task<TOut> Post<TOut>(string url, object dto, string authToken = "");

        Task<TOut> Put<TOut>(string url, object dto, string authToken = "");

        Task Delete(string url, string authToken = "");

        Task<string> Authenticate(string url, string email, string password);
    }
}
