using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Application.Security.ResponseModels
{
    public class AuthResponseModel 
    {        
        public string Token { get; set; }       
        public string UserName { get; set; }
    }
}
