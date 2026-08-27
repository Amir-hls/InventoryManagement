using Application.IRepository;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public DbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connnectionString);
        }
    }
}
