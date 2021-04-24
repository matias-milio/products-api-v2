using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Products.Application.Security.ResponseModels;
using Products.Application.Security.RequestModels;

namespace Products.API.Controllers
{
    [AllowAnonymous]
   
    public class UserLoginController : CustomBaseController
    {
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponseModel>> Login(LoginRequestModel credentials)
        {
            return await mediator.Send(credentials);
        }


    }
}
