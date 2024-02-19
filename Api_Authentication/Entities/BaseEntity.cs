using System.ComponentModel.DataAnnotations;

namespace Api_Authentication.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
