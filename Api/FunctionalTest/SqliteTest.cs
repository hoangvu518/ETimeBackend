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
using FluentAssertions;
using System.Net;

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

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //var data = await HttpHelpers.FromHttpResponseMessage<List<Employee>>(response);

            
        }

        [Fact]
        public async void Employee_Should_Be_Created()
        {
            var requestModel = new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "Hoang", LastName = "Pham", ManagerId = null, Salary = 10000 };
            var requestPayload = HttpHelpers.ToJson(requestModel);
            var response = await _client.PostAsync("api/Employee/Create", requestPayload);

            var path = response.Headers.Location.AbsolutePath;
            var getResponse = await _client.GetAsync(path);
            var data = await HttpHelpers.FromHttpResponseMessage<Employee>(getResponse);

            data.Email.Should().Be("Hoang@gmail.com");
            data.FirstName.Should().Be("Hoang");
            data.LastName.Should().Be("Pham");
            data.ManagerId.Should().BeNull();
            data.Salary.Should().Be(10000);
            
        }


        [Theory]
        [MemberData(nameof(TestData.BadCreateEmployeeDto), MemberType = typeof(TestData))]
        public async void Employee_Should_Not_Be_Created_With_Error_404(CreateEmployeeDto model)
        {
            var requestPayload = HttpHelpers.ToJson(model);
            
            var response = await _client.PostAsync("api/Employee/Create", requestPayload);

            var statusCode = response.StatusCode;

            statusCode.Should().Be(HttpStatusCode.BadRequest);
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
