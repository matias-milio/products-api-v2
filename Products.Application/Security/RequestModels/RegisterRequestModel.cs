using MediatR;

namespace Products.Application.Security.RequestModels
{
    public class RegisterRequestModel : IRequest<ResponseModels.RegisterResponseModel>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
