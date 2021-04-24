using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.Application.ErrorHandler;
using Products.Application.Interfaces;
using Products.Domain;
using Products.Infrastructure.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Security.Handlers
{
    public class Register : IRequestHandler<RequestModels.RegisterRequestModel,ResponseModels.RegisterResponseModel>
    {
        private readonly UserManager<User> UserManager;
        private readonly MyStoreDbContext context;
        private readonly IJwtFactory JwtFactory;
        public Register(UserManager<User> userManager,  IJwtFactory jwtFactory, MyStoreDbContext Context)
        {
            UserManager = userManager;
            context = Context;
            JwtFactory = jwtFactory;
        }     

        public async Task<ResponseModels.RegisterResponseModel> Handle(RequestModels.RegisterRequestModel request, CancellationToken cancellationToken)
        {
            bool userMailExists = await context.Users.Where(x => x.Email.Equals(request.Email)).AnyAsync();
            if (userMailExists)            
                throw new CustomException(HttpStatusCode.BadRequest, new { description = "User mail already exists" });

            bool userNameExists = await context.Users.Where(x => x.UserName.Equals(request.UserName)).AnyAsync();
            if (userNameExists)
                throw new CustomException(HttpStatusCode.BadRequest, new { description = "User name already exists" });

            var user = new User
            {
                UserName = request.UserName,
                FullName = request.Name + " " + request.Surname,
                Email = request.Email
            };

            var res = await UserManager.CreateAsync(user, request.Password);

            if (res.Succeeded)
                return new ResponseModels.RegisterResponseModel
                {                    
                    Token = JwtFactory.Create(user)
                };
            else
                throw new CustomException(HttpStatusCode.InternalServerError, new { description = "Registration failed" });
        }
    }
}
