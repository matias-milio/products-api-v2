using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Products.API.Extensions;

namespace Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersCustom();
            services.AddDbContext<MyStoreDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LocalConnectionString")));
            services.AddMediatR(typeof(Application.Products.Handlers.Get).Assembly);
            services.AddSwaggerGenCustom();
            services.AddIdentityCoreCustom();            
            services.AddJwtAuthentication(Configuration.GetSection("TokenSettings"));
            services.AddRedis(Configuration.GetSection("CacheSettings"));
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<Middleware.ErrorHandler>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatiasProductsAPI V1");           
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
