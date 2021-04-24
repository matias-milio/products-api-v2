using System;
using System.Collections.Generic;

#nullable disable

namespace Products.Domain
{
    public partial class ProductoBrand
    {
        public ProductoBrand()
        {
            Products = new HashSet<Product>();
        }
               
        public int ProductBrandId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
