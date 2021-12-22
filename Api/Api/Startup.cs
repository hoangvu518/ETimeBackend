using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Infrastructure;
using Api.Services;
using Api.Services.Interfaces;
using Api.StartupConfigs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _currentEnvironment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.MaxDepth = 1;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
            //.AddNewtonsoftJson(options => options.SerializerSettings.MaxDepth = );
            services.AddDbContext<TimeportalContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"))
                //.EnableSensitiveDataLogging();
                if (_currentEnvironment.IsDevelopment())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"))
                        .EnableSensitiveDataLogging(); ;
                }
                else
                {
                    options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"));
                }
            }
            );

            services.InjectDependencies();
            //uncomment for CSRF
            //services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))

            services.AddMvc()
                   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen();

            //uncomment for CSRF
            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage(); 
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                    //options.RoutePrefix = string.Empty;
                });
            }



            //uncomment for CSRF
            //app.Use(next => context =>
            //{
            //    string path = context.Request.Path.Value.ToLower();
            //    string[] directUrls = { "/GetAllEmployees" };
            //    if (path.StartsWith("/swagger") || path.StartsWith("/api") || string.Equals("/", path) || directUrls.Any(UrlHelperExtensions => path.StartsWith(UrlHelperExtensions)))

            //    {
            //        // The request token can be sent as a JavaScript-readable cookie, 
            //        // and Angular uses it by default.
            //        var tokens = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("X-XSRF-TOKEN", tokens.RequestToken,
            //            new CookieOptions() { HttpOnly = false });
            //    }

            //    return next(context);
            //});

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
