using Microsoft.AspNetCore.Identity;
using Products.Domain;
using Products.Infrastructure.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure
{
    public class TestData
    {
        public static async Task Insert(MyStoreDbContext context,UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    FullName = "Matias Gaudio",
                    UserName = "Madiox",
                    Email = "gaudiomatias@live.com"
                };

                await userManager.CreateAsync(user, "Xl332m.3435");
            }
           
        }
    }
}
