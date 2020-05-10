﻿using System;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.State
{
    public class UserStateImpl
    {
        private static readonly Lazy<UserStateImpl> _instance = new Lazy<UserStateImpl>(() => new UserStateImpl());

        private UserStateImpl()
        {
        }

        public static UserStateImpl Instance => _instance.Value;

        public LoggedInUserDto CurrentUser { get; set; }
    }
}
