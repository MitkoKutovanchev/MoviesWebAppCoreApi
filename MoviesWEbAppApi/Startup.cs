﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB;
using Data.DB.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MoviesWEbAppApi
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
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<MoviesWebAppDbContext>();

            services.AddMvc()
                .AddSessionStateTempDataProvider();

            services.AddMvcCore()
        .AddAuthorization()
        .AddJsonFormatters();

            services.AddAuthentication("Bearer")
        .AddIdentityServerAuthentication(options =>
        {
            options.Authority = "https://mitkosmovieswebappidentityserver.azurewebsites.net";
            options.RequireHttpsMetadata = false;
            options.ApiName = "Api1";
        });

            services.AddAuthorization(options => options.AddPolicy("admin", policy => policy.RequireClaim("admin", "admin")));

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSession();
                app.UseAuthentication();
                app.UseMvc();
            }

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MoviesWebAppDbContext>();
                DbInitializer.Initialize(context);
            }
        }
    }
}
