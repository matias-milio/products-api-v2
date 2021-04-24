using MediatR;
using Microsoft.AspNetCore.Identity;
using Products.Application.ErrorHandler;
using Products.Application.Interfaces;
using Products.Domain;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Security.Handlers
{
    public class Login : IRequestHandler<RequestModels.LoginRequestModel, ResponseModels.AuthResponseModel>
    {
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly IJwtFactory JwtFactory;
        public Login(UserManager<User> userManager, SignInManager<User> signInManager,IJwtFactory jwtFactory)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            JwtFactory = jwtFactory;
        }

        public async Task<ResponseModels.AuthResponseModel> Handle(RequestModels.LoginRequestModel request, CancellationToken cancellationToken)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);

            if (user == null)            
                throw new CustomException(HttpStatusCode.Unauthorized, new { description = "Email not found" });

            var res = await SignInManager.CheckPasswordSignInAsync(user, request.Password,false);

            if (res.Succeeded)            
                return new ResponseModels.AuthResponseModel {                   
                    UserName = user.UserName,
                    Token = JwtFactory.Create(user)
                };            
            else            
                throw new CustomException(HttpStatusCode.Unauthorized, new { description = "Validation failed" });
        }
    }
}
