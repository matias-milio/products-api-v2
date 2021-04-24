using MediatR;
using System;

namespace Products.Application.Products.RequestModels
{
    public class Delete : IRequest
    {
        public int Id { get; set; }
    }
}
