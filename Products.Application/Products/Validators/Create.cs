using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Products.Validators
{
    public class Create : AbstractValidator<RequestModels.Create>
    {
        public Create()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Stock).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
