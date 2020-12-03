using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.Models;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddRazorPages();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseRouting();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseElapsedTimeMiddleware();
            app.UseEndpoints((endpoints) =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Product}/{action=Index}");

                    endpoints.MapControllerRoute(
                        name: null,
                        pattern: "Product/List/{category}",
                        defaults: new { controller = "Product", action = "List" }
                        );
                    endpoints.MapControllerRoute(
                        name: null,
                        pattern: "{controller=Admin}/{action=Index}"
                        );
                });
        }
    }
}