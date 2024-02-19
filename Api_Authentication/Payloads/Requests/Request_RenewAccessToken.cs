using Microsoft.Identity.Client;

namespace Api_Authentication.Payloads.Requests
{
    public class Request_RenewAccessToken
    {
        public string RefreshToken { get; set; }

    }
}
