using JazzcashPortal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace JazzcashPortal.DAL
{
    public class AccountRepository
    {
        private readonly DbHelper _dbHelper;

        public AccountRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable JazzcashValidate(Account model)
        {
            DataTable dt;
            string query = "select * from sy_users where user_cd = :username and user_pass= :password and active='Y'";

            var parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("username", model.Username));
            parameters.Add(new OracleParameter("password", model.Password));

            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text, parameters.ToArray());
            return dt;

            //DataTable dt;
            //string query = "select * from sy_users where user_cd = '" + model.Username + "' and user_pass= '" + model.Password + "' and active='Y'";
            //dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            //return dt;
        }

        //public class LoginModel
        //{
        //    public required string Username { get; set; }
        //    public required string Password { get; set; }
        //    public string? JAZZCASH_USER_TYPE { get; set; }
        //}
    }
}
