using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Products.ResponseModels;
using Products.Infrastructure.AppDbContext;
using Products.Infrastructure.Intefaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Products.Handlers{
   
        public class Get: IRequestHandler<RequestModels.Get, List<ProductResponseModel>>
        {
            private readonly MyStoreDbContext context;
            private readonly ICacheManager _cacheManager;
            private readonly IMapper _mapper;
            private const string _cacheKey = "allProducts";

            public Get(MyStoreDbContext Context, ICacheManager cacheManager, IMapper mapper)
            {
                context = Context;
                _cacheManager = cacheManager;
                _mapper = mapper;
            }

            public async Task<List<ProductResponseModel>> Handle(RequestModels.Get request, CancellationToken cancellationToken)
            {
                var result = _cacheManager.GetOrSet(_cacheKey, () => context.Products.ToListAsync());
                return _mapper.Map<List<ProductResponseModel>>(await result);
            }
        }
}
