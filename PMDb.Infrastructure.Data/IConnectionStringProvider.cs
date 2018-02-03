using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public interface IConnectionStringProvider
    {
        string TestDbConnectionString { get; }
        string RealDbConnectionString { get; }
    }
}
