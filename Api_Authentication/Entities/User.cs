using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Authentication.Entities
{
    [Table("User_tbl")]
    [Index("UserName", IsUnique = true)]
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comments> Comments { get; set; }
    }
}
