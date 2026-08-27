using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Application.IRepository
{
    public interface ISqlConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
