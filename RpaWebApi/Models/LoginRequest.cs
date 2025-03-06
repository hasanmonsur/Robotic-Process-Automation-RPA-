namespace RpaWebApi.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string WebsiteUrl { get; set; } // URL of the target site
        public string DownloadUrl { get; set; } // URL of the file to download post-login
    }
}
