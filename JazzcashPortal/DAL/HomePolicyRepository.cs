using JazzcashPortal.Models;
using Microsoft.Data.SqlClient;
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

        public DataTable SearchHomePolicy(HomePolicy mdl)
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a";

            var parameters = new List<OracleParameter>();
            bool hasWhere = false;

            if (!string.IsNullOrEmpty(mdl.PERIOD_FROM) && !string.IsNullOrEmpty(mdl.PERIOD_TO))
            {
                query += " WHERE TRUNC(a.ent_date) BETWEEN TO_DATE(:PeriodFrom, 'DD-MON-YYYY') AND TO_DATE(:PeriodTo, 'DD-MON-YYYY')";
                parameters.Add(new OracleParameter("PeriodFrom", mdl.PERIOD_FROM));
                parameters.Add(new OracleParameter("PeriodTo", mdl.PERIOD_TO));
                hasWhere = true;
            }
            if (!string.IsNullOrEmpty(mdl.ContactNo))
            {
                query += hasWhere ? " AND " : " WHERE ";
                query += "phone_no = :ContactNo";
                parameters.Add(new OracleParameter("ContactNo", mdl.ContactNo));
            }
            query += hasWhere ? " AND " : " WHERE ";
            query += " func_assorted_string(a.policy_code) IS NOT NULL";
            query += " order by ent_date desc";

            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text, parameters.ToArray());
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


        //**************************************** Active Policies ****************************************
        public DataTable GetActivePolicies()
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a where a.status='A' order by ent_date desc";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }

        public DataTable SearchActivePolicy(HomePolicy mdl)
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a where a.status='A'";

            var parameters = new List<OracleParameter>();

            if (!string.IsNullOrEmpty(mdl.PERIOD_FROM) && !string.IsNullOrEmpty(mdl.PERIOD_TO))
            {
                query += " AND TRUNC(a.ent_date) BETWEEN TO_DATE(:PeriodFrom, 'DD-MON-YYYY') AND TO_DATE(:PeriodTo, 'DD-MON-YYYY')";
                parameters.Add(new OracleParameter("PeriodFrom", mdl.PERIOD_FROM));
                parameters.Add(new OracleParameter("PeriodTo", mdl.PERIOD_TO));
            }
            if (!string.IsNullOrEmpty(mdl.ContactNo))
            {
                query += " AND phone_no = :ContactNo";
                parameters.Add(new OracleParameter("ContactNo", mdl.ContactNo));
            }
            query += " AND func_assorted_string(a.policy_code) IS NOT NULL";
            query += " order by ent_date desc";

            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text, parameters.ToArray());
            return dt;
        }


        //**************************************** Cancel Policies ****************************************
        public DataTable GetCancelPolicies()
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a where a.status='R' order by ent_date desc";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }

        public DataTable SearchCancelPolicy(HomePolicy mdl)
        {
            DataTable dt;
            string query = "select func_assorted_string(a.policy_code) AssortedString, func_assorted_string(a.endorsement_code) Endorsement, a.* from ins_jazzcash_lead a where a.status='R'";

            var parameters = new List<OracleParameter>();

            if (!string.IsNullOrEmpty(mdl.PERIOD_FROM) && !string.IsNullOrEmpty(mdl.PERIOD_TO))
            {
                query += " AND TRUNC(a.ent_date) BETWEEN TO_DATE(:PeriodFrom, 'DD-MON-YYYY') AND TO_DATE(:PeriodTo, 'DD-MON-YYYY')";
                parameters.Add(new OracleParameter("PeriodFrom", mdl.PERIOD_FROM));
                parameters.Add(new OracleParameter("PeriodTo", mdl.PERIOD_TO));
            }
            if (!string.IsNullOrEmpty(mdl.ContactNo))
            {
                query += " AND phone_no = :ContactNo";
                parameters.Add(new OracleParameter("ContactNo", mdl.ContactNo));
            }
            query += " AND func_assorted_string(a.policy_code) IS NOT NULL";
            query += " order by ent_date desc";

            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text, parameters.ToArray());
            return dt;
        }
    }
}
