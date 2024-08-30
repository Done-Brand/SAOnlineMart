namespace SAOnlineMart.Models.DataModels
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
