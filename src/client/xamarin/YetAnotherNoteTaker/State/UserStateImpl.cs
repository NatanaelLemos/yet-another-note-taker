using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.State
{
    public class UserStateImpl
    {
        private static readonly Lazy<UserStateImpl> _instance = new Lazy<UserStateImpl>(() => new UserStateImpl());

        private UserStateImpl()
        {
        }

        public static UserStateImpl Instance => _instance.Value;

        public UserDto CurrentUser { get; set; }
    }
}
