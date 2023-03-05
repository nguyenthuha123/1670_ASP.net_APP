using System.ComponentModel.DataAnnotations;
namespace _1670_Final.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your Role")]
        public string Role { get; set; }
        //1 user co nhieu order
        
    }
}
