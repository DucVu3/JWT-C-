namespace Api_Authentication.Payloads.Requests
{
    public class ConfirmCreateNewPassword
    {
        public string CodeActive { get; set; }
        public string NewPassword { get; set; }
    }
}
