using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class ProductImage: CommonAbstract
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ảnh sản phẩm")]
        public string? Image { get;set; }
        [DisplayName("Mặc định")]
        public bool IsDefault { get; set; }
        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

    }
}
