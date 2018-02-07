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

        public ConnectionStringProvider(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public string TestDbConnectionString
        {
            get => _configuration.GetConnectionString("TestDBConnectionString"); 
        }
        public string RealDbConnectionString
        {
            get => throw new NotImplementedException();
        }
       
    }
}
