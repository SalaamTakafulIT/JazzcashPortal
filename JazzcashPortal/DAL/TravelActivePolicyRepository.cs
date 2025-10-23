using JazzcashPortal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace JazzcashPortal.DAL
{
    public class TravelActivePolicyRepository
    {
        private readonly DbHelper _dbHelper;

        public TravelActivePolicyRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable SearchTravelActivePolicy(TravelActivePolicy mdl)
        {
            DataTable dt;
            string query = "SELECT func_assorted_string(aa.policy_code) Policy_No,\r\n       aa.client_name,\r\n       aa.cnic,\r\n       aa.contact_no,\r\n       aa.from_date,\r\n       aa.to_date,\r\n       aa.no_of_days,\r\n     --  aa.departure_from,\r\n    --   aa.destination,\r\n      decode(aa.product_type,'B','BUS','I','INTERNATIONAL','D','DOMESTIC')Product_Type,\r\n       pp.plan_name,\r\n     --  c.city_name Dept_From,\r\n    --   dt.city_name Dept_To,\r\n     decode(aa.product_type,'I', conDF.Country_Name ,c.city_name)  Dept_From,\r\n     decode(aa.product_type,'I', conDT.Country_Name ,dt.city_name)  Dept_To\r\n      -- ,conDT.Country_Name Dept_ToC\r\n\r\n  FROM TMP_TRAVEL_POLICY       aa,\r\n       ins_assorted            a,\r\n       ins_travel_product_plan pp,\r\n       ins_city                c,\r\n       ins_city                dt,\r\n       ins_country             conDF,\r\n       ins_country             conDT\r\n where a.assorted_code = aa.policy_code\r\n   and a.policy_type_code <> '77'\r\n   and pp.plan_id = aa.plan_id\r\n   and pp.product_id = aa.product_id\r\n   and aa.departure_from = c.city_code(+)\r\n   and aa.destination = dt.city_code(+)\r\n   and aa.departure_from = conDF.country_code(+)\r\n   and aa.destination = conDT.Country_Code(+)\r\n   and aa.ent_by = 'JAZCASH'\r\n   and aa.policy_code is not null";

            var parameters = new List<OracleParameter>();

            if (!string.IsNullOrEmpty(mdl.PERIOD_FROM) && !string.IsNullOrEmpty(mdl.PERIOD_TO))
            {
                query += " and TRUNC(aa.ent_date) BETWEEN TO_DATE(:PeriodFrom, 'DD-MON-YYYY') AND TO_DATE(:PeriodTo, 'DD-MON-YYYY')";
                parameters.Add(new OracleParameter("PeriodFrom", mdl.PERIOD_FROM));
                parameters.Add(new OracleParameter("PeriodTo", mdl.PERIOD_TO));
            }
            if (!string.IsNullOrEmpty(mdl.ContactNo))
            {
                query += " and aa.CONTACT_NO = :ContactNo";
                parameters.Add(new OracleParameter("ContactNo", mdl.ContactNo));
            }
            if (!string.IsNullOrEmpty(mdl.ProductType) && mdl.ProductType != "00")
            {
                query += " and aa.Product_Type = :ProductType";
                parameters.Add(new OracleParameter("ProductType", mdl.ProductType));
            }
            query += " order by aa.ent_date";

            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text, parameters.ToArray());
            return dt;
        }
    }
}
