using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Dtos
{
    public class LoggedInUserDto : UserDto
    {
        public string AccessToken { get; set; }
    }
}
