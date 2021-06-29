using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DatabaseOperations.Interfaces
{
    public interface IEbDataOps
    {
        DataTable GetDataTable(string connectionString, SqlParameter[] sqlParameters, string storedProcedure);
    }
}
