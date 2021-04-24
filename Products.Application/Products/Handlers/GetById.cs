using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Products.Infrastructure.AppDbContext;
using Products.Application.ErrorHandler;
using System.Net;
using Products.Application.Products.ResponseModels;
using AutoMapper;

namespace Products.Application.Products.Handlers
{
    public class GetById : IRequestHandler<RequestModels.GetById, ProductResponseModel>
    {
        private readonly MyStoreDbContext context;
        private readonly IMapper _mapper;

        public GetById(MyStoreDbContext Context, IMapper mapper)
        {
            context = Context;
            _mapper = mapper;
        }

        public async Task<ProductResponseModel> Handle(RequestModels.GetById request, CancellationToken cancellationToken)
        {
            var product = context.Products.FindAsync(request.Id);

            if (product == null)
                throw new CustomException(HttpStatusCode.NotFound, new { description = "Product not found" });

            return _mapper.Map<ProductResponseModel>(await product);
        }
    }
}
