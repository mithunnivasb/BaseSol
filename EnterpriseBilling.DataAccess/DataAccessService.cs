using EnterpriseBilling.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EnterpriseBilling.DataAccess
{
     public class DataAccessService : IDataAcessService
    {
        public DataAccessService()
        {
            
        }

        /// <summary>
        /// All select statements will be done with SqlDataReader.  Used for calling stored procdured that
        /// hopefully just returns a result set and does not update anything.  However, it could be used 
        /// to call a stored procedure that issues an insert or update and returns a result set.
        /// </summary>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns SqlDataReader with data from Select statement. </returns>
        public SqlDataReader executeReader(SqlParameter[] param, string storedProcedureName)
        {
            return executeReader(null, param, storedProcedureName);
        }

        /// <summary>
        /// All Select statements will be done with SqlDataReader.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns SqlDataReader with data from Select statement. </returns>
        public SqlDataReader executeReader(string connString, SqlParameter[] param, string storedProcedureName)
        {
            SqlDataReader dataReader = null;
            SqlConnection connection = null;
            SqlCommand command = null;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);
                connection.Open();

                dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw ex;
            }

            return dataReader;
        }

        /// <summary>
        /// All Select statements will be done with Datatable.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns SqlDataReader with data from Select statement. </returns>
        public DataTable getDataTable(string connString, SqlParameter[] param, string storedProcedureName)
        {
            DataTable objDataTable = new DataTable();
            SqlConnection connection = null;
            SqlCommand command = null;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null)
                    {
                        foreach (SqlParameter parameters in param)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(objDataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw ex;
            }

            return objDataTable;
        }


        /// <summary>
        /// To update a table use ExecuteNonQuery.
        /// </summary>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns integer for error checking. </returns>
        public int updateTable(SqlParameter[] param, string storedProcedureName)
        {
            return updateTable(null, param, storedProcedureName);
        }

        /// <summary>
        /// To update a table use ExecuteNonQuery.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns integer for error checking. </returns>
        public int updateTable(string connString, SqlParameter[] param, string storedProcedureName)
        {
            int returnValue;
            SqlConnection connection = null;
            SqlCommand command = null;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);
                connection.Open();

                returnValue = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                throw ex;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// Executes a stored procedure which deletes a row, ExecuteNonQuery.
        /// </summary>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns integer for error checking. </returns>
        public int deleteRow(SqlParameter[] param, string storedProcedureName)
        {
            return deleteRow(null, param, storedProcedureName);
        }

        /// <summary>
        /// Executes a stored procedure which deletes a row, ExecuteNonQuery.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns integer for error checking. </returns>
        public int deleteRow(string connString, SqlParameter[] param, string storedProcedureName)
        {
            int returnValue;
            SqlConnection connection = null;
            SqlCommand command = null;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);
                connection.Open();

                returnValue = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                throw ex;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
            }
            return returnValue;
        }

        /// <summary>
        /// To insert a new value and return its primary key field ID, ExecuteScalar.
        /// </summary>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns the primary key of the inserted value, if the called procedure returns that value.</returns>
        public int insertNewRecord(SqlParameter[] param, string storedProcedureName)
        {
            return insertNewRecord(null, param, storedProcedureName);
        }

        /// <summary>
        /// To insert a new value and return its primary key field ID, ExecuteScalar.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns the primary key of the inserted value, if the called procedure returns that value.</returns>
        public int insertNewRecord(string connString, SqlParameter[] param, string storedProcedureName)
        {
            int returnValue = 0;
            SqlConnection connection = null;
            SqlCommand command = null;
            string returnVal = string.Empty;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);
                connection.Open();

                returnVal = Convert.ToString(command.ExecuteScalar());
                returnValue = (returnVal == "") ? 0 : Convert.ToInt32(returnVal);
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                throw ex;
            }
            finally
            {
                //if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// Executes a stored procedure and returns first row's first column value.
        /// </summary>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns the first row's first column value. </returns>
        public object getSingleValue(SqlParameter[] param, string storedProcedureName)
        {
            return getSingleValue(null, param, storedProcedureName);
        }

        /// <summary>
        /// Executes a stored procedure and returns first row's first column value.
        /// </summary>
        /// <param name="connString"> Specifies server and database names and password. </param>
        /// <param name="param"> Parameters to be passed to the procedure. </param>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <returns> Returns the first row's first column value. </returns>
        public object getSingleValue(string connString, SqlParameter[] param, string storedProcedureName)
        {
            object returnValue = null;
            SqlConnection connection = null;
            SqlCommand command = null;

            SetConnection(connString, param, storedProcedureName, out connection, out command);

            try
            {
                logStoredProcedureCall(storedProcedureName, param, connection.DataSource, connection.Database);

                connection.Open();
                returnValue = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                logStoredProcedureException(ex);
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return returnValue;
        }

        //private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Reports any SQL calls before running them.
        /// </summary>
        /// <param name="storedProcedureName"> Name of the stored procedure about to be executed. </param>
        /// <param name="parameters"> Parameters to be passed to the procedure. </param>
        /// <param name="server"> Server name from the connection string. </param>
        /// <param name="database"> Database name from the connection string. </param>
        private static void logStoredProcedureCall(string storedProcedureName, SqlParameter[] parameters, string server, string database)
        {
            //if (log.IsDebugEnabled)
            //{
            //    string schema = "dbo";
            //    if (storedProcedureName.Contains("."))
            //    {
            //        schema = string.Empty;
            //    }

            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendFormat("EXEC {0}.{1}.{2}.{3}", server, database, schema, storedProcedureName);

            //    foreach (var param in parameters)
            //    {
            //        sb.Append(" " + paramNameValue(param));
            //    }

            //    log.Debug(sb.ToString().TrimEnd(','));
            //}
        }

        /// <summary>
        /// Reports any SQL exceptions when running the command.
        /// </summary>
        /// <param name="ex"> Exception. </param>
        private static void logStoredProcedureException(Exception ex)
        {
            //if (ex is SqlException)
            //{
            //    log.Error(ex.Message);
            //}
            //else
            //{
            //    log.Error(ex);
            //}
        }

        /// <summary>
        /// Turn parameter into SQL value.
        /// </summary>
        /// <param name="param"> SQL parameter. </param>
        /// <returns></returns>
        private static string paramNameValue(SqlParameter param)
        {
            string value = null;

            if (param.Value != null)
            {
                value = param.Value.ToString();
            }

            switch (param.SqlDbType)
            {
                case SqlDbType.BigInt:
                    break;
                case SqlDbType.Binary:
                    break;
                case SqlDbType.Bit:
                    break;
                case SqlDbType.Date:
                    break;
                case SqlDbType.DateTime:
                    break;
                case SqlDbType.DateTime2:
                    break;
                case SqlDbType.DateTimeOffset:
                    break;
                case SqlDbType.Decimal:
                    break;
                case SqlDbType.Float:
                    break;
                case SqlDbType.Image:
                    break;
                case SqlDbType.Int:
                    break;
                case SqlDbType.Money:
                    break;
                case SqlDbType.Real:
                    break;
                case SqlDbType.SmallDateTime:
                    break;
                case SqlDbType.SmallInt:
                    break;
                case SqlDbType.SmallMoney:
                    break;
                case SqlDbType.Structured:
                    break;
                case SqlDbType.Time:
                    break;
                case SqlDbType.Timestamp:
                    break;
                case SqlDbType.TinyInt:
                    break;
                case SqlDbType.Udt:
                    break;
                case SqlDbType.UniqueIdentifier:
                    break;
                case SqlDbType.VarBinary:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Char:
                case SqlDbType.VarChar:
                case SqlDbType.Text:
                    value = "'" + param.Value + "'";
                    break;
                case SqlDbType.Variant:
                    break;
                case SqlDbType.Xml:
                    break;
                default:
                    break;
            }

            return string.Format("@{0} = {1},", param.ParameterName, value);
        }

        /// <summary>
        /// Creates a connection and command based on connection string, stored procedure, and parameters.
        /// </summary>
        /// <param name="connString"> Connection string shorthand. </param>
        /// <param name="param"> Parameters to pass to procedure. </param>
        /// <param name="storedProcedureName"> Procedure to run in SQL. </param>
        public void SetConnection(string connString, SqlParameter[] param, string storedProcedureName, out SqlConnection connection, out SqlCommand command)
        {
            //commenting this line as there is no concept of configuration manager in .net 5, we can use Iconfigration manager.

            //var connStringFull = getConnectionString(connString);

            connection = new SqlConnection(connString);

            command = new SqlCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 600,
            };

            if (param != null)
            {
                command.Parameters.AddRange(param);
            }
        }

        /// <summary>
        /// Turn shorthand connection string into full string through webconfig or appconfig.
        /// </summary>
        /// <param name="connString"> Shorthand connection string. </param>
        /// <returns></returns>
        private static string getConnectionString(string connString)
        {
            string cStringKey;
            try
            {
                cStringKey = ConfigurationManager.AppSettings["ActiveDB"];
                connString = cStringKey != null ? cStringKey : connString;
            }
            catch
            {
                //do nothing use the connStringKey 
            }

            // Moved connection strings from appSetting to connectionString web.config section so we could encrypt them.
            var connectionStringEntry = ConfigurationManager.ConnectionStrings[connString];

            string connectionString = null;

            // Fallback to looking for connection string in app settings temporarily
            if (connectionStringEntry != null)
            {
                connectionString = connectionStringEntry.ConnectionString;
            }            

            if (connectionString == null)
            {
                throw new ApplicationException(string.Format("Could not find connection string in web.config for key '{0}'", connString));
            }

            return connectionString;
        }
    }
}
