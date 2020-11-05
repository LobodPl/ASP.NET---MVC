using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public virtual Category Category { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public String Desc { get; set; }
    }
}