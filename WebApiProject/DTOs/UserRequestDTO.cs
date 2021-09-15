using WebApiProject.enums;

namespace WebApiProject.DTOs
{
    public class UserRequestDTO : UserRequestWithouRoleDTO
    {
        public Roles Role { get; set; }
        
        
    }
}