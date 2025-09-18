namespace JazzcashPortal.Models
{
    public class Account
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? JAZZCASH_USER_TYPE { get; set; }
    }
}
