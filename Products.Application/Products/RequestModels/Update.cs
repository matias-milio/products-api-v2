using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Products.RequestModels
{
    public class Update : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
    }
}
