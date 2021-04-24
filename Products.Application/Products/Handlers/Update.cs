using MediatR;
using Products.Application.ErrorHandler;
using Products.Infrastructure.AppDbContext;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Products.Handlers
{
    public class Update : IRequestHandler<RequestModels.Update>
    {
        private readonly MyStoreDbContext context;
        public Update(MyStoreDbContext Context)
        {
            context = Context;
        }

        public async Task<Unit> Handle(RequestModels.Update request, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(request.Id,cancellationToken);

            if (product == null)
                throw new CustomException(HttpStatusCode.NotFound, new { description = "Product not found" });

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;
            product.Stock = request.Stock ?? product.Stock;            

            var res = await context.SaveChangesAsync(cancellationToken);

            if (res > 0)
                return Unit.Value;
            else
                throw new CustomException(HttpStatusCode.InternalServerError, new { description = "Transaction not completed" });
        }
    }
}
