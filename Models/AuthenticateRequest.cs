using System.ComponentModel.DataAnnotations;

namespace AgoraTokenServer.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string channel { get; set; }
        [Required]
        public dynamic uid { get; set; }

        public uint expiredTs { get; set; } = 0;

        public int role { get; set; } = 1;
    }
}