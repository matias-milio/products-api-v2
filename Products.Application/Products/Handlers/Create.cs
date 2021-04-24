using MediatR;
using Products.Application.ErrorHandler;
using Products.Domain;
using Products.Infrastructure.AppDbContext;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Products.Handlers
{
    public class Create : IRequestHandler<RequestModels.Create>
    {
        private readonly MyStoreDbContext context;
        public Create(MyStoreDbContext Context)
        {
            context = Context;
        }

        public async Task<Unit> Handle(RequestModels.Create request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,                
                Stock = request.Stock
            };

            await context.AddAsync(product);
            var res = await context.SaveChangesAsync(cancellationToken);

            if (res > 0)
                return Unit.Value;
            else
                throw new CustomException(HttpStatusCode.InternalServerError, new { description = "Transaction not completed" });
        }
    }
}
