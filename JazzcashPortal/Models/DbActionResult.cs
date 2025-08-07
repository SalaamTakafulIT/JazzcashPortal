namespace JazzcashPortal.Models
{
    public class DbActionResult
    {
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public bool Action { get; set; }
        public int Value { get; set; }
    }
}
