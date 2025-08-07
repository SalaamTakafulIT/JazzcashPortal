using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace JazzcashPortal.Models
{
    public class DbHelper
    {
        private readonly string _connectionString;
        private DataSet m_dbDataSet = new DataSet();
        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        }
        public DataSet GetDataSet
        {
            get
            {
                return m_dbDataSet;
            }
        }
        public DataTable ExecQueryReturnTable(string query, CommandType commandType, OracleParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                        connection.Close();
                    }
                }
            }
            return dataTable;
        }
        public bool ExecuteNonQuery(string query, CommandType type, OracleParameter[] parameters = null)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.CommandType = type;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DbActionResult SaveChanges(string query, CommandType commandType, OracleParameter[] parameters = null)
        {
            DbActionResult result = new DbActionResult { Action = false };
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        result.Action = true;
                        result.Message = $"{rowsAffected} row(s) affected.";
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = ex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }


        public DbActionResult SaveChangesTransaction(string query, CommandType commandType, OracleParameter[] parameters = null)
        {
            DbActionResult result = new DbActionResult { Action = false };
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        connection.Open();
                        var val = command.ExecuteScalar();
                        int num = val != null ? Convert.ToInt32(val) : 0;
                        result.Value = num;
                        result.Action = true;
                        result.Message = "data inserted.";
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = ex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public bool ExecuteQuery(string pstrOracle)
        {
            bool executionStatus = false;

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand(pstrOracle, connection))
                {
                    command.CommandType = CommandType.Text;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        executionStatus = true;
                    }
                    catch (Exception ex)
                    {
                        // You can optionally log the exception here
                        executionStatus = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return executionStatus;
        }
        public DataTable BindGrid(string commandText, CommandType commandType, OracleParameter[] parameters)
        {
            using (var conn = new OracleConnection(_connectionString))
            using (var cmd = new OracleCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (var da = new OracleDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
