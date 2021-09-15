using WebApiProject.enums;

namespace WebApiProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}