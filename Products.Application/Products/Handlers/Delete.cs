using MediatR;
using Products.Application.ErrorHandler;
using Products.Infrastructure.AppDbContext;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Products.Application.Products.Handlers
{
    public class Delete : IRequestHandler<RequestModels.Delete>
    {
        private readonly MyStoreDbContext context;
        public Delete(MyStoreDbContext Context)
        {
            context = Context;
        }

        public async Task<Unit> Handle(RequestModels.Delete request, CancellationToken cancellationToken)
        {
            var product = context.Products.FindAsync(request.Id);

            if (product == null)            
                throw new CustomException(HttpStatusCode.NotFound, new { description = "Product not found" });
            
            context.Remove(product);

            var res = await context.SaveChangesAsync(cancellationToken);

            if (res > 0)
                return Unit.Value;
            else
                throw new CustomException(HttpStatusCode.InternalServerError, new { description = "Transaction not completed" });
        }
    }
}
