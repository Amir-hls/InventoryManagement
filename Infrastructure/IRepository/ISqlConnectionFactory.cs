using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.IRepository
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection(string connectionString);
    }
}
