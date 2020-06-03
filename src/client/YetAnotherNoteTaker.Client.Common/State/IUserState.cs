using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.State
{
    public interface IUserState
    {
        Task<bool> IsAuthenticated();

        Task<bool> IsAuthenticated(Type pageType);

        Task<string> UserEmail { get; }

        Task<string> Token { get; }

        Task SetUser(LoggedInUserDto user);
    }
}
