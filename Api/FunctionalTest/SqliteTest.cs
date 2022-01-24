using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FunctionalTest
{
    public class SqliteTest : IClassFixture<SqliteWebApplicationFactory<Api.Startup>>
    {
        private readonly HttpClient _client;
        private readonly SqliteWebApplicationFactory<Api.Startup> _factory;
        public SqliteTest(SqliteWebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });


            //var test = context.Employee.ToList();
        }

        [Fact]
        public async void Test1()
        {

            var response = await _client.GetAsync("api/Employee/GetAll");

            var data = await HttpService.FromHttpResponseMessage<List<Employee>>(response);
        }

        [Fact]
        public async void Test2()
        {

            var scope = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<TimeportalContext>();
            context.Employee.Add(new Employee("Hoang", "Pham", null, 100, "hoang@gmail.com"));
            context.SaveChanges();
           
        }
        [Fact]
        public async void Test3()
        {


            var scope = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<TimeportalContext>();
            context.Employee.Add(new Employee("Hoang", "Pham", null, 100, "hoang@gmail.com"));
            context.SaveChanges();
        }
        [Fact]
        public async void IfYouNeedAccessToDBCOntext()
        {
            var scope = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<TimeportalContext>();
            var test = context.Employee.ToList();

        }
    }
}
