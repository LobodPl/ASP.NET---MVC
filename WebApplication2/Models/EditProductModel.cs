using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class EditProductModel
    {
        public Product product { get; set; }
        public IQueryable<Category> Categories { get; set; }
    }
}