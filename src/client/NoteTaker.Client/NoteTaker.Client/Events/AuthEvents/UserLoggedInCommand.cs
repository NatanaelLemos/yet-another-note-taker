using System;
using System.Collections.Generic;
using System.Text;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.AuthEvents
{
    public class UserLoggedInCommand
    {
        public UserLoggedInCommand(UserDto currentUser)
        {
            CurrentUser = currentUser;
        }

        public UserDto CurrentUser { get; }
    }
}
