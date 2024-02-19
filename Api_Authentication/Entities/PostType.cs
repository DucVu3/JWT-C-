using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Authentication.Entities
{
    [Table("Post_Type_tbl")]
    public class PostType : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
