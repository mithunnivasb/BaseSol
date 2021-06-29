using DatabaseOperations.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DatabaseOperations.Implementation
{
    public class EbDataOps : IEbDataOps
    {
        public DataTable GetDataTable(string connectionString, SqlParameter[] sqlParameters, string storedProcedure)
        {
            DataTable resultData = new DataTable();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in sqlParameters)
                        {
                            cmd.Parameters.Add(param);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();

                            da.Fill(dt);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resultData;
        }
    }
}
