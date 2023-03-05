using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace _1670_Final.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter quantity book you want to borrow")]
        public int Qty { get; set; }
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter your phoneNumber")]
        public string Phone { get; set; }

        public DateTime OrderTime { get; set; }

        public int BookId { get; set; }

        public string IdentityUserId { get; set; }

        //cho phep lam khac base

        public virtual Book? book { get; set; }

        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
