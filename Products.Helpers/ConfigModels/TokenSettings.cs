namespace Products.Helpers.ConfigModels
{
    public class TokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
