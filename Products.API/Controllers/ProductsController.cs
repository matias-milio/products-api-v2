using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Application.Products.RequestModels;
using MediatR;
using Products.Application.Products.ResponseModels;

namespace Products.API.Controllers
{
    [ApiController]    
    public class ProductsController : CustomBaseController
    {    
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseModel>>> Get()
        {
            return await mediator.Send(new Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetById(int id)
        {
            return await mediator.Send(new GetById { Id = id });
        }

        [HttpPost]
        public async Task<Unit> Create(Create newProduct)
        {
            return await mediator.Send(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<Unit> Create(int id, Update changedProduct)
        {
            changedProduct.Id = id;
            return await mediator.Send(changedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> Delete(int id)
        {          
            return await mediator.Send(new Delete { Id = id});
        }

    }
}
