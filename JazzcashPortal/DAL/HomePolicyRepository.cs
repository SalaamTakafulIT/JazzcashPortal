using JazzcashPortal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace JazzcashPortal.DAL
{
    public class HomePolicyRepository
    {
        private readonly DbHelper _dbHelper;

        public HomePolicyRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        //**************************************** Home Policy ****************************************
        public DataTable GetLeads()
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a order by ent_date desc";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }

        //public bool ReversePolicy(string id, string user_id)
        //{
        //    bool return_value = false;
        //    OracleParameter[] param = new OracleParameter[]
        //    {
        //        new OracleParameter("@p_unique_id", OracleDbType.NVarchar2, id, ParameterDirection.Input),
        //        new OracleParameter("@P_Generate_against", OracleDbType.NVarchar2, "060000000364", ParameterDirection.Input),
        //        new OracleParameter("@p_user_code", OracleDbType.NVarchar2, user_id, ParameterDirection.Input)
        //    };

        //    return_value = _dbHelper.ExecuteNonQuery("proc_Household_Certificate", CommandType.StoredProcedure, param);
        //    return return_value;
        //}

        public bool ReversePolicy(string policy_code)
        {
            OracleParameter[] param = new OracleParameter[]
            {
                new OracleParameter("p_assorted_code", OracleDbType.NVarchar2, ParameterDirection.Input)
                {
                    Value = policy_code
                },
                new OracleParameter("p_Ent_by", OracleDbType.NVarchar2, ParameterDirection.Input)
                {
                    Value = "Jazzcash"
                }
            };

            return _dbHelper.ExecuteNonQuery("proc_Household_ENDORSEMENT", CommandType.StoredProcedure, param);
        }
    }
}
