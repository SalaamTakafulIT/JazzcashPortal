using JazzcashPortal.Models;
using System.Data;

namespace JazzcashPortal.DAL
{
    public class DashboardRepository
    {
        private readonly DbHelper _dbHelper;

        public DashboardRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable GetDashboardData()
        {
            DataTable dt;
            string query = "SELECT SUM(CASE WHEN status = 'A' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) THEN 1 ELSE 0 END) AS YTD_Active_Sale, SUM(CASE WHEN status = 'A' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) THEN amount ELSE 0 END) AS YTD_Active_Premium, SUM(CASE WHEN status = 'R' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) THEN 1 ELSE 0 END) AS YTD_Refund_Sale, SUM(CASE WHEN status = 'R' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) THEN amount ELSE 0 END) AS YTD_Refund_Premium, SUM(CASE WHEN status = 'A' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) AND EXTRACT(MONTH FROM ent_date) = EXTRACT(MONTH FROM SYSDATE) THEN 1 ELSE 0 END) AS MTD_Active_Sale, SUM(CASE WHEN status = 'A' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) AND EXTRACT(MONTH FROM ent_date) = EXTRACT(MONTH FROM SYSDATE) THEN amount ELSE 0 END) AS MTD_Active_Premium, SUM(CASE WHEN status = 'R' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) AND EXTRACT(MONTH FROM ent_date) = EXTRACT(MONTH FROM SYSDATE) THEN 1 ELSE 0 END) AS MTD_Refund_Sale, SUM(CASE WHEN status = 'R' AND EXTRACT(YEAR FROM ent_date) = EXTRACT(YEAR FROM SYSDATE) AND EXTRACT(MONTH FROM ent_date) = EXTRACT(MONTH FROM SYSDATE) THEN amount ELSE 0 END) AS MTD_Refund_Premium, SUM(CASE WHEN status = 'A' AND TRUNC(ent_date) = TRUNC(SYSDATE) THEN 1 ELSE 0 END) AS Today_Active_Sale, SUM(CASE WHEN status = 'A' AND TRUNC(ent_date) = TRUNC(SYSDATE) THEN amount ELSE 0 END) AS Today_Active_Premium, SUM(CASE WHEN status = 'R' AND TRUNC(ent_date) = TRUNC(SYSDATE) THEN 1 ELSE 0 END) AS Today_Refund_Sale, SUM(CASE WHEN status = 'R' AND TRUNC(ent_date) = TRUNC(SYSDATE) THEN amount ELSE 0 END) AS Today_Refund_Premium FROM ins_jazzcash_lead";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }
    }
}
