using Api_Authentication.DataContext;
using Api_Authentication.Entities;
using Api_Authentication.Payloads.DTOs;

namespace Api_Authentication.Payloads.Converters
{
    public class UserConverter
    {
        private readonly AppDbContext _context;
        public UserConverter()
        {
            _context = new AppDbContext();
        }
        public UserDTO EntityToDTO(User user)
        {
            return new UserDTO
            {
                Email = user.Email,
                Avatar = user.Avatar,
                DateOfBirth = user.DateOfBirth,
                FullName = user.FullName,
                UserName = user.UserName,
                RoleName = _context.roles.SingleOrDefault(x => x.Id == user.RoleId).RoleName
            };
        }
    }
}
