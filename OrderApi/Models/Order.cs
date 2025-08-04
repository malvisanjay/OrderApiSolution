namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string LabelNo { get; set; }
        public string Carrier { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string SenderStreet { get; set; }
        public string SenderCity { get; set; }
        public string SenderPostCode { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverPostCode { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}