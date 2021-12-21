using Api.Infrastructure;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ExternalServices.Interfaces;

namespace Api.StartupConfigs
{
    public static class DependencyInjection
    {
        public static void InjectDependencies(this IServiceCollection services)
        {


            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddHttpContextAccessor();
        }
    }
}
