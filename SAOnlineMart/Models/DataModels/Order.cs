namespace SAOnlineMart.Models.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } //  "Pending", "Shipped", "Completed" Will explain

    }


}
