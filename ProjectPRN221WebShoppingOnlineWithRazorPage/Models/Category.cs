using System.ComponentModel.DataAnnotations;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class Category: CommonAbstract
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        [StringLength(250)]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [Required(ErrorMessage = "Icon của thể loại không được để trống")]
        public string? Icon { get; set; }

    }
}
