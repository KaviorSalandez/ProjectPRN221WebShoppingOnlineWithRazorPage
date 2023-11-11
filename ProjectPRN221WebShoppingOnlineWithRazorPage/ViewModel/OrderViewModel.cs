using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.ViewModel
{
    public class OrderViewModel
    {
        public string? UserId { get; set; }
        [DisplayName("Tên khách hàng")]

        public string? CustomerName { get; set; }
        [Required]

        [DisplayName("Số điện thoại")]

        public string? PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [DisplayName("Địa chỉ nhận hàng")]

        public string? Address { get; set; }
        [DisplayName("Ghi chú")]

        public string? Comment { get; set; } 
    }
}
