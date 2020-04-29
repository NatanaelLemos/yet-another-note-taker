﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class AuthService : IAuthService
    {
        public static List<UserDto> Users = new List<UserDto>
        {
            new UserDto{ Email = "e", Id = Guid.NewGuid() }
        };

        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task CreateUser(NewUserDto newUserDto)
        {
            Users.Add(new UserDto
            {
                Id = Guid.NewGuid(),
                Email = newUserDto.Email
            });

            return Task.CompletedTask;
        }

        public Task<UserDto> Login(string email, string password)
        {
            var result = Users.FirstOrDefault(u => u.Email == email);

            if (result == null)
            {
                throw new NullReferenceException("Invalid email or password");
            }

            return Task.FromResult(result);
        }
    }
}
