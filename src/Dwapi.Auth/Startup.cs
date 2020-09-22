// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dwapi.Auth
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Environment = environment;
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var clients = Configuration.GetSection("Clients").Get<ApiClient[]>();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients(clients));

            if (Environment.IsDevelopment())
            {
                // not recommended for production - you need to store your key material somewhere secure
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddCertificateFromFile(Configuration);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHsts();
            // uncomment if you want to add MVC
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
        }
    }
}
