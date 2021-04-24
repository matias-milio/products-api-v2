using FluentValidation;

namespace Products.Application.Security.Validators
{
    public class Register : AbstractValidator<RequestModels.RegisterRequestModel>
    {
        public Register()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Surname).NotEmpty().NotNull();
        }
    }
}
