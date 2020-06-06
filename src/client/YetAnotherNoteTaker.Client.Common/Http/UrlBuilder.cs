namespace YetAnotherNoteTaker.Client.Common.Http
{
    public interface IUrlBuilder
    {
        UsersUrlBuilder Users { get; }

        NotebooksUrlBuilder Notebooks { get; }

        NotesUrlBuilder Notes { get; }
    }

    public class UrlBuilder : IUrlBuilder
    {
        private readonly string _urlBase;

        public UrlBuilder(string urlBase)
        {
            _urlBase = urlBase;
        }

        public UsersUrlBuilder Users => new UsersUrlBuilder(_urlBase);

        public NotebooksUrlBuilder Notebooks => new NotebooksUrlBuilder(_urlBase);

        public NotesUrlBuilder Notes => new NotesUrlBuilder(_urlBase);
    }

    public class UsersUrlBuilder
    {
        private readonly string _urlBase;

        public UsersUrlBuilder(string urlBase)
        {
            _urlBase = urlBase;
        }

        public string Post => $"{_urlBase}/v0/users";

        public string Auth => $"{_urlBase}/connect/token";
    }

    public class NotebooksUrlBuilder
    {
        private readonly string _urlBase;

        public NotebooksUrlBuilder(string urlBase)
        {
            _urlBase = urlBase;
        }

        public string GetAll(string email)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks";
        }

        public string Get(string email, string notebookKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}";
        }

        public string Post(string email)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks";
        }

        public string Put(string email, string notebookKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}";
        }

        public string Delete(string email, string notebookKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}";
        }
    }

    public class NotesUrlBuilder
    {
        private readonly string _urlBase;

        public NotesUrlBuilder(string urlBase)
        {
            _urlBase = urlBase;
        }

        public string GetAll(string email)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/notes";
        }

        public string GetByNotebookKey(string email, string notebookKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}/notes";
        }

        public string Get(string email, string notebookKey, string noteKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}/notes/{noteKey}";
        }

        public string Post(string email, string notebookKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}/notes";
        }

        public string Put(string email, string notebookKey, string noteKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}/notes/{noteKey}";
        }

        public string Delete(string email, string notebookKey, string noteKey)
        {
            return $"{_urlBase}/v0/users/{email}/notebooks/{notebookKey}/notes/{noteKey}";
        }
    }
}
