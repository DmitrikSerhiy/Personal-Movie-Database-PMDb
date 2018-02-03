using Microsoft.EntityFrameworkCore;
using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class MovieContext : DbContext
    {

        private string connectionString = null;
        public MovieContext(IConnectionStringProvider connectionStringProvider)
        {
            connectionString = connectionStringProvider.TestDbConnectionString;
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Movie> Movies { get; set; }

    }
}
