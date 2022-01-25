using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace FunctionalTest
{
    public class EmployeeControllerTest : IClassFixture<InmemoryWebApplicationFactory<Api.Startup>>
    {
        private readonly HttpClient _client;
        private readonly InmemoryWebApplicationFactory<Api.Startup> _factory;
        public EmployeeControllerTest(InmemoryWebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });



        }

        [Fact]
        public async void Test1Async()
        {

            var response = await _client.GetAsync("api/Employee/GetAll");

            var data = await HttpHelpers.FromHttpResponseMessage<List<Employee>>(response);

        }

        [Fact]
        public async void IfYouNeedAccessToDBCOntext()
        {
            var scope = _factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<TimeportalContext>();
        }
    }
}
