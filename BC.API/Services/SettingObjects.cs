namespace BC.API.Services
{
    public class TokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }
    }

    public class SocialMediaAuthCredentials
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUrl { get; set; }
    }

    public class RocketSMSOptions
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
