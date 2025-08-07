using JazzcashPortal.DAL;
using JazzcashPortal.Models;
using JazzcashPortal.Models.Setups;
using System.Data;

namespace JazzcashPortal.BLL
{
    public class ProductConfigurationService
    {
        private readonly ProductConfigurationRepository _dal;
        public ProductConfigurationService(ProductConfigurationRepository dal)
        {
            _dal = dal;
        }

        //**************************************** ProductConfigurationService ****************************************
        public DataTable GetProductConfig()
        {
            return _dal.GetProductConfig();
        }

        public DataTable GetProductConfigById(string product_id)
        {
            return _dal.GetProductConfigById(product_id);
        }

        public DbActionResult SaveProductConfiguration(ProductConfiguration mdl)
        {
            var result = _dal.SaveProductConfiguration(mdl);
            if (result.Action)
            {
                result.Message = "Record Added Successfully.";
            }
            else
            {
                result.ErrorMessage = "Error Occured.";
            }
            return result;
        }

        public DbActionResult UpdateProductConfiguration(ProductConfiguration mdl)
        {
            var result = _dal.UpdateProductConfiguration(mdl);
            if (result.Action)
            {
                result.Message = "Record Updated Successfully.";
            }
            else
            {
                result.ErrorMessage = "Error Occured.";
            }
            return result;
        }

        public DbActionResult DeleteProductConfiguration(string Id)
        {
            var result = _dal.DeleteProductConfiguration(Id);
            if (result.Action)
            {
                result.Message = "Record Deleted Successfully.";
            }
            else
            {
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorMessage.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    result.ErrorMessage = "Cannot delete this record because it is referenced in another table.";
                }
                else
                {
                    result.ErrorMessage = "Error Occured.";
                }
            }
            return result;
        }
    }
}
