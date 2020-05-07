using Raven.Client.Documents.Session;

namespace YetAnotherNoteTaker.Server.Data
{
    public class UsersRepository
    {
        private readonly IAsyncDocumentSession _session;

        public UsersRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }
    }
}
