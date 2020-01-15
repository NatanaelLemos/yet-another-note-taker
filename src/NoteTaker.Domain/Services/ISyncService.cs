using System.Threading.Tasks;

namespace NoteTaker.Domain.Services
{
    public interface ISyncService
    {
        Task<string> GetMessages();

        Task UpdateMessages(string message);
    }
}
