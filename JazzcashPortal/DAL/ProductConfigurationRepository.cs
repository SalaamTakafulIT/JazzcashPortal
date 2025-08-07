using JazzcashPortal.Models;
using JazzcashPortal.Models.Setups;
using System.Data;

namespace JazzcashPortal.DAL
{
    public class ProductConfigurationRepository
    {
        private readonly DbHelper _dbHelper;

        public ProductConfigurationRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        //**************************************** ProductConfigurationRepository ****************************************
        public DataTable GetProductConfig()
        {
            DataTable dt;
            string query = "select * from ins_jc_product_confi";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }

        public DataTable GetProductConfigById(string id)
        {
            DataTable dt;
            string query = "select * from ins_jc_product_confi where product_id='" + id + "'";
            dt = _dbHelper.ExecQueryReturnTable(query, CommandType.Text);
            return dt;
        }

        public DbActionResult SaveProductConfiguration(ProductConfiguration m)
        {
            string query = @"INSERT INTO ins_jc_product_confi 
                (PRODUCT_ID, PRODUCT_NAME, SUMCOVERED, NET_CONTRIBUTION)
                VALUES
                ((select MAX(PRODUCT_ID)+1 from ins_jc_product_confi), '" + m.PRODUCT_NAME + "', '" + m.SUMCOVERED + "', '" + m.NET_CONTRIBUTION + "')";
            var dbResult = _dbHelper.SaveChanges(query, CommandType.Text);
            return dbResult;
        }

        public DbActionResult UpdateProductConfiguration(ProductConfiguration m)
        {
            string query = @"UPDATE ins_jc_product_confi
            SET PRODUCT_NAME = '" + m.PRODUCT_NAME + "', SUMCOVERED = '" + m.SUMCOVERED + "', NET_CONTRIBUTION = '" + m.NET_CONTRIBUTION + "' WHERE PRODUCT_ID = '" + m.PRODUCT_ID + "'";
            var dbResult = _dbHelper.SaveChanges(query, CommandType.Text);
            return dbResult;
        }

        public DbActionResult DeleteProductConfiguration(string Id)
        {
            string query = @"DELETE FROM ins_jc_product_confi WHERE PRODUCT_ID = '" + Id + "'";
            var dbResult = _dbHelper.SaveChanges(query, CommandType.Text);
            return dbResult;
        }
    }
}
