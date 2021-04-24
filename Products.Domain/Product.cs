using System;
using System.Collections.Generic;

#nullable disable

namespace Products.Domain
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryIdFK { get; set; }
        public double Price { get; set; }
        public int ProductBrandIdFK { get; set; }
        public int Stock { get; set; }

        public virtual ProductoBrand ProductBrand { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
