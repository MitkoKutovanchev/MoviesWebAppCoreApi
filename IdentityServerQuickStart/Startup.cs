using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerQuickStart
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
            .AddTestUsers(Config.GetUsers())
            .AddProfileService<ProfileService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }

        public class ProfileService : IProfileService
        {
            public Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                context.IssuedClaims.AddRange(context.Subject.Claims);

                return Task.FromResult(0);
            }

            public Task IsActiveAsync(IsActiveContext context)
            {
                return Task.FromResult(0);
            }
        }
    }
}

