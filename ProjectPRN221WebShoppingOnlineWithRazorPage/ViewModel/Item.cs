namespace ProjectPRN221WebShoppingOnlineWithRazorPage.ViewModel
{
    public class Item
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ImageOfProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
    }
}
