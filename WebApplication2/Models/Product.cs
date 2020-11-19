using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required, StringLength(100)]
        public String Name { get; set; }

        [Required, Range(1, 1000000000)]
        public decimal Price { get; set; }

        [Required, StringLength(10000)]
        [Display(Name = "Description")]
        public String Desc { get; set; }
    }
}