﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Models
{
    // khi báo AppUser để sau này có thể bổ sung thêm các trường User như địa chỉ nhà riêng,...
    // tạm thời ở đây cta ko cho propety nào
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Orders = new HashSet<Order>();
        }
        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string? HomeAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string? Avatar { get;set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
