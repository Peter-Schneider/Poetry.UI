using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poetry.UI.AspNetCore;
using Website.CategorySupport;
using Website.ProductSupport;
using Website.RoutingSupport;

namespace Website.AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPoetryUI().AddAssembly(typeof(WebsiteComponent).Assembly).Done();
            services.AddMvc();
            services.AddTransient<UrlProvider, UrlProvider>();
            services.AddTransient<CategoryRepository, CategoryRepository>();
            services.AddTransient<ProductRepository, ProductRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UsePoetryUI();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                "PoetryPortal",
                "Admin",
                new { controller = "PoetryPortal", action = "Index" });

                routes.MapRoute(
                    name: "Default",
                    template: "",
                    defaults: new { controller = "Home", action = "Index" }
                );

                routes.MapRoute(
                    name: "ViewCategory",
                    template: "{category}",
                    defaults: new { controller = "Category", action = "Index" }
                );

                routes.MapRoute(
                    name: "ViewProduct",
                    template: "{category}/{product}",
                    defaults: new { controller = "Product", action = "Index" }
                );
            });
        }
    }
}
