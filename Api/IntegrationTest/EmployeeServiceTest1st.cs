using Api.Core.Interfaces;
using Api.Core.Services;
using Api.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTest
{
    //Simple test. Need to new up dbcontext in every test method. Not optimal
    public class EmployeeServiceTest1st
    {

        [Fact]
        public async Task Test1Async()
        {
            using var context = await Builder.GetDbContext() ;
            EmployeeService employeeService = new EmployeeService(context);
            var createdEmployee = await employeeService.CreateEmployeeAsync("Hoang", "Pham", null, 100, "hoang@gmail.com");
            var count = await context.Employee.CountAsync();
            count.Should().Be(1);
            
        }

        [Fact]
        public async Task Test2Async()
        {
            using var context = await Builder.GetDbContext();
            EmployeeService employeeService = new EmployeeService(context);
            var createdEmployee = await employeeService.CreateEmployeeAsync("Hoang", "Pham", null, 100, "hoang@gmail.com");
            var count = await context.Employee.CountAsync();
            count.Should().Be(1);

        }

        

        ////[Fact]
        ////public async Task Test3Async()
        ////{
        ////    var connection = new SqliteConnection("Filename=:memory:");
        ////    connection.Open();

        ////    var builder = new DbContextOptionsBuilder<TimeportalContext>()
        ////        .UseSqlite(connection);

        ////    using (var context = new TimeportalContext(builder.Options))
        ////    {

        ////    }


        ////}
    }
}
