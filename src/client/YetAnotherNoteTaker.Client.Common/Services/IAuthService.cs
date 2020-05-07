﻿using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface IAuthService
    {
        Task CreateUser(NewUserDto newUserDto);

        Task<UserDto> Login(string email, string password);
    }
}