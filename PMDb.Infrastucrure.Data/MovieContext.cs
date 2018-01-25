using Microsoft.EntityFrameworkCore;
using PMDb.Domain.Core;
using System;

namespace PMDb.Infrastucrure.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

    }
}
