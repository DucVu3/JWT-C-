using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Authentication.Entities
{
    [Table("Comments_tbl")]
    public class Comments : BaseEntity
    {
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; }
        public DateTime RemoveTime { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
