using WebApiProject.enums;

namespace WebApiProject.DTOs
{
    public class UserRequestDto : UserRequestWithouRoleDto
    {
        public Roles Role { get; set; }
        
        
    }
}