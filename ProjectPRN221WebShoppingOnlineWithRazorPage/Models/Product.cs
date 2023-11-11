using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class Product: CommonAbstract
    {
        public Product()
        {
            this.ProductImages  = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();


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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]

        public double? OriginalPrice { get; set; }
        [DisplayName("Giá bán")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]

        public double? Price { get; set; }
        [DisplayName("Giá khi giảm")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be equal or greater than 0.")]

        public double? PriceSale { get; set; }


        [DisplayName("Số lượng trong kho")]
        [Range(0, 100, ErrorMessage = "Số lượng phải từ 0-100")]
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

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }



    }
}
