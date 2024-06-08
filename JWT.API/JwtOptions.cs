namespace JWT.API
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public TimeSpan ExpiredTime { get; set; }
        public string Secret { get; set; }
    }
}
