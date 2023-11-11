using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]    
        public virtual Order Order { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
