using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EnterpriseBilling.Models.Interfaces
{
    //this needs decoupling
    public interface IDataAcessService
    {
        int deleteRow(SqlParameter[] param, string storedProcedureName);
        int deleteRow(string connString, SqlParameter[] param, string storedProcedureName);
        SqlDataReader executeReader(SqlParameter[] param, string storedProcedureName);
        SqlDataReader executeReader(string connString, SqlParameter[] param, string storedProcedureName);
        DataTable getDataTable(string connString, SqlParameter[] param, string storedProcedureName);
        object getSingleValue(SqlParameter[] param, string storedProcedureName);
        object getSingleValue(string connString, SqlParameter[] param, string storedProcedureName);
        int insertNewRecord(SqlParameter[] param, string storedProcedureName);
        int insertNewRecord(string connString, SqlParameter[] param, string storedProcedureName);
        void SetConnection(string connString, SqlParameter[] param, string storedProcedureName, out SqlConnection connection, out SqlCommand command);
        int updateTable(SqlParameter[] param, string storedProcedureName);
        int updateTable(string connString, SqlParameter[] param, string storedProcedureName);
    }
}
