using Api.Core.Services;
using Api.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class EmployeeServiceTestLocalDB: IDisposable
    {
        private TimeportalContext _context;
        public EmployeeServiceTestLocalDB()
        {
            //var options = new DbContextOptionsBuilder<TimeportalContext>().UseSqlite("DataSource=:memory:").Options;

            var options = new DbContextOptionsBuilder<TimeportalContext>().UseSqlServer(
              "Server=(localdb)\\MSSQLLocalDB;Database=TimePortal;Trusted_Connection=True;").Options;
            

            _context = new TimeportalContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        [Fact]
        public async Task Test1Async()
        {
            EmployeeService employeeService = new EmployeeService(_context);
            var createdEmployee = await employeeService.CreateEmployeeAsync("Hoang", "Pham", null, 100, "hoang@gmail.com");
            var count = await _context.Employee.CountAsync();
            count.Should().Be(1);

        }

        [Fact]
        public async Task Test2Async()
        {
            EmployeeService employeeService = new EmployeeService(_context);
            var createdEmployee = await employeeService.CreateEmployeeAsync("Hoang", "Pham", null, 100, "hoang@gmail.com");
            var count = await _context.Employee.CountAsync();
            count.Should().Be(1);

        }
    }
}
