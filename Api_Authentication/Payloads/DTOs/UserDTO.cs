using Api_Authentication.Entities;

namespace Api_Authentication.Payloads.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
