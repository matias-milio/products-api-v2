using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Security.Validators
{
    public class Login : AbstractValidator<RequestModels.LoginRequestModel>
    {
        public Login()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();  
        }
    }
}
