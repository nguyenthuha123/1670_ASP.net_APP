using System.ComponentModel.DataAnnotations;

namespace _1670_Final.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter book title")]
        [MinLength(2, ErrorMessage = "Title must have at least 2 characters")]
        [MaxLength(100, ErrorMessage = "Max length of title is 20 characters")]
        public string NameBook { get; set; }

        [Required(ErrorMessage = "Please enter book author")]
        [MinLength(2, ErrorMessage = "Title must have at least 2 characters")]
        [MaxLength(30, ErrorMessage = "Max length of title is 20 characters")]
        public string Book_author { get; set; }

        [Required(ErrorMessage = "please enter publisher")]
        [MinLength(2, ErrorMessage = "Publisher must have at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Publisher is 20 characters")]
        public string Book_publisher { get; set; }

        [Required(ErrorMessage = "Please enter BookDate")]
        [DataType(DataType.Date)]
        public DateTime Book_date { get; set; }

        [Required(ErrorMessage = "Please enter BookPrice")]
        public double Book_price { get; set; }

        [Required(ErrorMessage = "Enter book image link here")]
        public string Book_image { get; set; }

        [Required(ErrorMessage = "Please enter book language")]
        public string Book_language { get; set; }

        [Required(ErrorMessage = "Please enter BookDescription")]
        public string Book_description { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        public int Book_quantity { get; set; }

        [Required(ErrorMessage = "Please enter name category")]
        public string? Category { get; set; }

        public virtual ICollection<Order>? Order { get; set; }
    }
}
