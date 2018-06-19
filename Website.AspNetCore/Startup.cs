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
using Website.DomainObjects;
using Website.ProductSupport;

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
            services.AddPoetryUI().AddAssembly(typeof(DomainObjectsComponent).Assembly).Done();
            services.AddMvc();
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
