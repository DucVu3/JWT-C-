namespace Api_Authentication.Payloads.Responses
{
    public class DataResponseUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public string Avatar { get; set; }
        public string FullName { get; set; }
    }
}
