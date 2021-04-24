using MediatR;
using Products.Domain;

namespace Products.Application.Security.RequestModels
{
    public class LoginRequestModel : IRequest<ResponseModels.AuthResponseModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
