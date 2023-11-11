namespace ProjectPRN221WebShoppingOnlineWithRazorPage.ViewModel
{
    public class ShoppingCart
    {
        // khởi tạo 1 list gồm các cartitem
        public List<Item> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<Item>();
        }
        public void AddToCart(Item item, int Quantity)
        {
            // cần kiểm tra sản phẩm đã có trong list này chưa
            var checkExists = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExists != null)
            {
                checkExists.Quantity += Quantity;
                checkExists.PriceTotal = checkExists.Quantity * checkExists.Price;
            }
            else
            {
                Items.Add(item);
            }
        }

        // hàm xóa 1 item trong giỏ hàng
        public void Remove(int id)
        {
            var checkExists = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExists != null)
            {
                Items.Remove(checkExists);
            }
        }

        // hàm cập nhật đơn hàng
        public void UpdateQuantity(int id, int Quantity)
        {
            var checkExists = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExists != null)
            {
                checkExists.Quantity = Quantity;
                checkExists.PriceTotal = checkExists.Price * checkExists.Quantity;
            }
        }
        // hàm lấy ra tổng số tiền của đơn hàng
        public decimal GetPriceTotal()
        {
            return Items.Sum(x => x.PriceTotal);
        }
        // lấy ra tổng số lượng
        public decimal GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }

        // xóa sạch giỏ hàng
        public void ClearCart()
        {
            Items.Clear();
        }

    }

   
}
