namespace Api_Authentication.Payloads.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime RemoveTime { get; set; }
        public UserDTO User { get; set; }
    }
}
