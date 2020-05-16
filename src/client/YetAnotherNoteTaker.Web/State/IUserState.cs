using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YetAnotherNoteTaker.Web.State
{
    public interface IUserState
    {
        Task<bool> IsAuthenticated();
        Task<string> GetUserEmail();
        Task SetUserEmail(string email);
        Task<string> GetToken();
        Task SetToken(string token);
        Task SetShouldUpdate(bool shouldUpdate);
        Task<bool> ShouldUpdate();
    }
}
