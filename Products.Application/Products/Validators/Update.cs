using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Products.Validators
{
    public class Update : AbstractValidator<RequestModels.Update>
    {
        public Update()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Stock).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
