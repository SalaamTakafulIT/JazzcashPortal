namespace JazzcashPortal.Models
{
    public class Jazzcash
    {
        public string? TRANSACTION_ID { get; set; }
        public string? REFERENCE_NO { get; set; }
        public string? X_CLIENT_ID { get; set; }
        public string? X_CLIENT_SECRET { get; set; }
        public string? X_PARTNER_ID { get; set; }
        public string? Secret_Key { get; set; }
        public string? IV { get; set; }
    }

    public class JazzcashResult
    {
        public bool action { get; set; }
        public string? message { get; set; }
        public string? error_message { get; set; }
        public string? status_code { get; set; }
        public string? transaction_id { get; set; }
    }
}
