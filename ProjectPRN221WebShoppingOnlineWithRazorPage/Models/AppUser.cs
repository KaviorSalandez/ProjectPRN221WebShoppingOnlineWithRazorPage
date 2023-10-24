using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    // khi báo AppUser để sau này có thể bổ sung thêm các trường User như địa chỉ nhà riêng,...
    // tạm thời ở đây cta ko cho propety nào
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string? HomeAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}
