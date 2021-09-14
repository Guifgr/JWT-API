using System.Threading.Tasks;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserRequestDTO> GetUserByUsername(string userName);
        Task<UserRequestDTO> ValidateUser(UserRequestDTO user);
        Task<UserRequestDTO> CreateUser(UserRequestDTO user);
        Task<UserRequestDTO> UpdateUser(UserRequestDTO user);
        Task DeleteUser(string userName);
    }
}