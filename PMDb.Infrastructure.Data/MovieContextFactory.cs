using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PMDb.Domain.Interfaces;
using PMDb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class MovieContextFactory : IDesignTimeDbContextFactory<MovieContext>
    {
        private IConfiguration Configuration { get; set; }


        public MovieContext CreateDbContext(string[] args)
        {
            return new MovieContext();
        }
    }
}
