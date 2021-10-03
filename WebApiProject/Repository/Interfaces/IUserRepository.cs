using System.Threading.Tasks;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserRequestDto> GetUserByUsername(string userName);
        Task<UserRequestDto> ValidateUser(UserRequestDto user);
        Task<UserRequestDto> CreateUser(UserRequestDto user);
        Task<UserRequestDto> UpdateUser(UserRequestDto user);
        Task DeleteUser(string userName);
    }
}