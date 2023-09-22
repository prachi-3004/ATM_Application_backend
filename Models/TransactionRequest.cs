namespace ATM.Models
{
    public class TransactionRequest
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientId { get; set; }
        public string Pin {  get; set; }
    }
}
