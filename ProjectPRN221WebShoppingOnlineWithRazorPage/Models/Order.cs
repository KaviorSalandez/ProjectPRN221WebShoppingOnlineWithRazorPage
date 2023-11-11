using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    public class Order :CommonAbstract
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]  
        public AppUser? AppUser { get; set; }   



        [Required]
        public string? Code { get; set; }

        public double OrderTotal { get; set; }

        public string? Comment { get; set; }
       
        public string? Status { get; set; }

        public string? ShipAddress { get; set; }

        public string? PhoneNumber { get; set; }    

        public string? Email { get; set; }

        public string? SessionId { get; set; }  

        public string? PaymentIntentId { get; set; }  

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
