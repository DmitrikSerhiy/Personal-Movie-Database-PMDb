using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IConnectionStringProvider
    {
        string TestDbConnectionString { get; }
        string RealDbConnectionString { get; }
    }
}
