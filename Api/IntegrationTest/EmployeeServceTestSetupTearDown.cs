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
    public class EmployeeServceTestSetupTearDown: IDisposable
    {
        private TimeportalContext _context;
        public EmployeeServceTestSetupTearDown()
        {
            var options = new DbContextOptionsBuilder<TimeportalContext>().UseSqlite("DataSource=:memory:").Options;
            var dbContext = new TimeportalContext(options);

            // SQLite needs to open connection to the DB.
            // Not required for in-memory-database and MS SQL.
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();

            _context = dbContext;
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
