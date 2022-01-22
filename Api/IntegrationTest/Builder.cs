using Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public static class Builder
    {
        public static async Task<TimeportalContext> GetDbContext()
        {
            //DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            
            //   // Use Sqlite DB.
            //builder.UseSqlite("DataSource=:memory:", null);

            var options = new DbContextOptionsBuilder<TimeportalContext>().UseSqlite("DataSource=:memory:").Options;


            var dbContext = new TimeportalContext(options);

            // SQLite needs to open connection to the DB.
            // Not required for in-memory-database and MS SQL.
            await dbContext.Database.OpenConnectionAsync();


            await dbContext.Database.EnsureCreatedAsync();
            return dbContext;
        }
    }
}
