namespace JazzcashPortal.Models
{
    public class Jazzcash
    {
        public string? X_URL { get; set; }
        public string? TRANSACTION_ID { get; set; }
        public string? REFERENCE_NO { get; set; }
        public string? X_CLIENT_ID { get; set; }
        public string? X_CLIENT_SECRET { get; set; }
        public string? X_PARTNER_ID { get; set; }
        public string? Secret_Key { get; set; }
        public string? IV { get; set; }
    }


    public class SubApiResponse
    {
        public string? transactionId { get; set; }
        public string? timeStamp { get; set; }
        public string? resultCode { get; set; }
        public string? resultDesc { get; set; }
        public string? failedReason { get; set; }
        public string? referenceid { get; set; }
        public string? posId { get; set; }
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
