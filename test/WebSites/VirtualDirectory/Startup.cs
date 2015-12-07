﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VirtualDirectory
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            services.AddSwaggerGen();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.Map("/vdir", subApp =>
            {
                // Add the platform handler to the request pipeline.
                subApp.UseIISPlatformHandler();

                // Configure the HTTP request pipeline.
                subApp.UseStaticFiles();

                // Add MVC to the request pipeline.
                subApp.UseMvc();
                // Add the following route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");

                subApp.UseSwaggerGen();
                subApp.UseSwaggerUi(swaggerUrl: "/vdir/swagger/v1/swagger.json");
            });
        }
    }
}