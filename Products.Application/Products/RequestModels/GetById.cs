using MediatR;
using Products.Application.Products.ResponseModels;

namespace Products.Application.Products.RequestModels
{
    public class GetById : IRequest<ProductResponseModel>
    {
        public int Id { get; set; }
    }
}
