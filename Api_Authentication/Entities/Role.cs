using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Authentication.Entities
{
    [Table("Role_tbl")]
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
        public List<User>? Users { get; set; }
    }
}
