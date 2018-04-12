using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;
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
        //public static readonly LoggerFactory loggerFactory
        //    = new LoggerFactory(new[]
        //    {
        //        new ConsoleLoggerProvider( (category, level)
        //            => category == DbLoggerCategory.Database.Command.Name
        //            && level == LogLevel.Information, true)
        //    });


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }



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
                    //.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging(true)
                    .UseSqlServer(connectionString);
            }
            else
            {
                optionsBuilder.
                    UseSqlServer(new ConnectionStringProvider().GetConnectionStringDirectly());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieListMovie>()
                .HasKey(t => new { t.MovieId, t.MovieListId });

            modelBuilder.Entity<MovieListMovie>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.MovieListMovie)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<MovieListMovie>()
                .HasOne(sc => sc.MovieList)
                .WithMany(c => c.MovieListMovies)
                .HasForeignKey(sc => sc.MovieListId);



            modelBuilder.Entity<MovieActor>()
                .HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(sc => sc.Actor)
                .WithMany(s => s.MovieActor)
                .HasForeignKey(sc => sc.ActorId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(sc => sc.Movie)
                .WithMany(c => c.MovieActor)
                .HasForeignKey(sc => sc.MovieId);



            modelBuilder.Entity<MovieDirector>()
                .HasKey(t => new { t.DirectorId, t.MovieId });

            modelBuilder.Entity<MovieDirector>()
                .HasOne(sc => sc.Director)
                .WithMany(s => s.MovieDirector)
                .HasForeignKey(sc => sc.DirectorId);

            modelBuilder.Entity<MovieDirector>()
                .HasOne(sc => sc.Movie)
                .WithMany(c => c.MovieDirector)
                .HasForeignKey(sc => sc.MovieId);



            modelBuilder.Entity<MovieGenre>()
                .HasKey(t => new { t.GenreId, t.MovieId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.MovieGenre)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(sc => sc.Genre)
                .WithMany(c => c.MovieGenre)
                .HasForeignKey(sc => sc.GenreId);



            modelBuilder.Entity<MovieTag>()
                .HasKey(t => new { t.TagId, t.MovieId });

            modelBuilder.Entity<MovieTag>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.MovieTag)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<MovieTag>()
                .HasOne(sc => sc.Tag)
                .WithMany(c => c.MovieTag)
                .HasForeignKey(sc => sc.TagId);



            modelBuilder.Entity<MovieWriter>()
                .HasKey(t => new { t.WriterId, t.MovieId });

            modelBuilder.Entity<MovieWriter>()
                .HasOne(sc => sc.Movie)
                .WithMany(s => s.MovieWriter)
                .HasForeignKey(sc => sc.MovieId);

            modelBuilder.Entity<MovieWriter>()
                .HasOne(sc => sc.Writer)
                .WithMany(c => c.MovieWriter)
                .HasForeignKey(sc => sc.WriterId);
        }
    }
}
