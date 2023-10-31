using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class Product: CommonAbstract
    {
        public Product()
        {
            ProductImages  = new HashSet<ProductImage>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} là bắt buộc")]
        [DisplayName("Tiêu đề")]
        public string? Title { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string? ProductCode { get; set; }
        [DisplayName("Mô tả")]

        public string? Description { get; set; }
        [DisplayName("Chi tiết sản phẩm")]

        public string? Detail { get; set; }
        [DisplayName("Ảnh")]

        public string? Image { get; set; }
        [DisplayName("Giá nhập")]

        public double? OriginalPrice { get; set; }
        [DisplayName("Giá bán")]

        public double? Price { get; set; }
        [DisplayName("Giá khi giảm")]

        public double? PriceSale { get; set; }
        [DisplayName("Số lượng trong kho")]

        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [DisplayName("Danh mục")]
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }




    }
}
