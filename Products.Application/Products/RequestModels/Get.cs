using MediatR;
using Products.Application.Products.ResponseModels;
using System.Collections.Generic;

namespace Products.Application.Products.RequestModels
{
    public class Get : IRequest<List<ProductResponseModel>>
    {
    }
}
