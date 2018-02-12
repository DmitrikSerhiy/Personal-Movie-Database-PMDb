using Microsoft.Extensions.Configuration;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private IConfiguration _configuration;
        private string CurrentConnectionString = "TestDBConnectionString"; //the only place to set connection string

        public ConnectionStringProvider(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public ConnectionStringProvider()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string GetConnectionStringDirectly()
        {
            return _configuration.GetConnectionString(CurrentConnectionString);
        }

        public string TestDbConnectionString
        {
            get => _configuration.GetConnectionString(CurrentConnectionString); 
        }
        public string RealDbConnectionString
        {
            get => throw new NotImplementedException();
        }
       
    }
}
