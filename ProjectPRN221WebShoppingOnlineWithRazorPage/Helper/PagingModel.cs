
namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Helper
{
    public class PagingModel
    {
        // trang hiện tại
        public int currentpage { get; set; }    
        // tổng số trang
        public int countpages { get; set; }
        // nhận 1 tham số int và trả về một url
        public Func<int?, string> generateUrl { get; set; } 
    }
}
