using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.DTOs;
using WebApiProject.enums;
using WebApiProject.Models;
using WebApiProject.Repository.Interfaces;

namespace WebApiProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectContext _context;

        public UserRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task<UserRequestDto> GetUserByUsername(string userName)
        {
            return await _context.Users
                .AsNoTracking()
                .Select(u => new UserRequestDto
                {
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                })
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<UserRequestDto> ValidateUser(UserRequestDto user)
        {
            var userEntity = await GetUserByUsername(user.Username);
            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, userEntity.Password))
            {
                return new UserRequestDto
                {
                    Username = userEntity.Username,
                    Password = userEntity.Password,
                    Role = userEntity.Role
                };
            }

            throw new AuthenticationException();
        }

        public async Task<User> GetUserByUsernameTracking(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<UserRequestDto> CreateUser(UserRequestDto user)
        {
            var hasUser = await GetUserByUsername(user.Username);
            if (hasUser != default)
            {
                throw new BadHttpRequestException("User already existis");
            }
            await _context.Users.AddAsync(new User
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                Role = user.Role
            });
            await _context.SaveChangesAsync();
            return new UserRequestDto
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };
        }

        public async Task<UserRequestDto> UpdateUser(UserRequestDto user)
        {
            var userEntity = await GetUserByUsernameTracking(user.Username);
            
            if (userEntity == default)
            {
                return default;
            }
            
            userEntity.Username = user.Username;
            userEntity.Password = user.Password;
            await _context.SaveChangesAsync();
            return new UserRequestDto()
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                Role = user.Role
            };
        }

        public async Task DeleteUser(string userName)
        {
            var userEntity = await GetUserByUsernameTracking(userName);
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}