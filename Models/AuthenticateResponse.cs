namespace AgoraTokenServer.Models
{
    public class AuthenticateResponse
    {
        public string channel { get; set; }
        public dynamic uid { get; set; }
        public string token { get; set; }
    }
}