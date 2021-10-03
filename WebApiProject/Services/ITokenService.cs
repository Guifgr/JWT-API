using WebApiProject.DTOs;

namespace WebApiProject.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserRequestDto user);
    }
}