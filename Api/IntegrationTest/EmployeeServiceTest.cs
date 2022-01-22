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
    public class EmployeeServiceTest
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
            count.Should().Be(2);

        }

            //using (var connection = new SqliteConnection("DataSource=:memory:"))
            //{
            //    connection.Open();

            //    var options = new DbContextOptionsBuilder<TimeportalContext>()
            //        .UseSqlite(connection) // Set the connection explicitly, so it won't be closed automatically by EF
            //        .Options;

            //    // Create the dabase schema
            //    // You can use MigrateAsync if you use Migrations
            //    using (var context = new TimeportalContext(options))
            //    {
            //        await context.Database.EnsureCreatedAsync();
            //    } // The connection is not closed, so the database still exists

            //    using (var context = new TimeportalContext(options))
            //    {
            //        context.Employee.Add(new Api.Models.Employee("Hoang", "Pham", null, 100, "hoang@gmail.com"));
            //        EmployeeService employeeService = new EmployeeService(context);

            //        var createdEmployee = await employeeService.CreateEmployeeAsync("Hoang", "Pham", null, 100, "hoang@gmail.com");
            //        createdEmployee.FirstName.Should().Be("Hoang");
            //    }

            //}
        

        [Fact]
        public async Task Test3Async()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<TimeportalContext>()
                .UseSqlite(connection);

            using (var context = new TimeportalContext(builder.Options))
            {

            }

            //var options = new DbContextOptionsBuilder<TimeportalContext>().UseInMemoryDatabase("Test").Options;
            //using var dbContext = new TimeportalContext(options);
            //await dbContext.Employee.AddAsync((new Api.Models.Employee("Hoang", "Pham", null, 100, "hoang@gmail.com")));
            //await dbContext.SaveChangesAsync();

            //var count = await dbContext.Employee.CountAsync();
            //count.Should().Be(1);




        }
    }
}
