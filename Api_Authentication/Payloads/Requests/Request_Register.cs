using Api_Authentication.Entities;

namespace Api_Authentication.Payloads.Requests
{
    public class Request_Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
