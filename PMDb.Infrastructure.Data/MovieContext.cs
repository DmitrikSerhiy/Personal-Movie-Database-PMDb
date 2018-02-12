using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class MovieContext : DbContext
    {

        private string connectionString = null;
        public static readonly LoggerFactory loggerFactory
            = new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider( (category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information, true)
            });


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }



        public MovieContext() //ctor for migrations
        {
            Database.EnsureCreated();
        }

        public MovieContext(IConnectionStringProvider connectionStringProvider)
        {
            connectionString = connectionStringProvider.TestDbConnectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString != null)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging(true)
                    .UseSqlServer(connectionString);
            }
            else
            {
                optionsBuilder.
                    UseSqlServer(new ConnectionStringProvider().GetConnectionStringDirectly());
            }
        }
    }
}
