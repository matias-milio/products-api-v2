using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Products.RequestModels
{
    public class Create : IRequest
    {        
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public double Price { get; set; }
        public int ProductBrandId { get; set; }
        public int Stock { get; set; }
    }
}
