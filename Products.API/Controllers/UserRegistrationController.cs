using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Products.Application.Security.RequestModels;
using Products.Application.Security.ResponseModels;

namespace Products.API.Controllers
{
    [AllowAnonymous]
    public class UserRegistrationController : CustomBaseController
    {
        [HttpPost]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterRequestModel newCredentials)
        {
            return await mediator.Send(newCredentials);
        }
    }
}
