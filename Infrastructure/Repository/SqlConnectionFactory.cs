using Infrastructure.IRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repository
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string? _connnectionString;
        public SqlConnectionFactory(string? connnectionString)
        {
            _connnectionString = connnectionString;
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
