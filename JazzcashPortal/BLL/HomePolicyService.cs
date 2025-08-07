using JazzcashPortal.DAL;
using JazzcashPortal.Models;
using System.Data;

namespace JazzcashPortal.BLL
{
    public class HomePolicyService
    {
        private readonly HomePolicyRepository _dal;
        public HomePolicyService(HomePolicyRepository dal)
        {
            _dal = dal;
        }

        //**************************************** HomePolicyService ****************************************
        public DataTable GetLeads()
        {
            return _dal.GetLeads();
        }

        public DbActionResult ReversePolicy(string policy_code)
        {
            var dbar = new DbActionResult();
            bool output = _dal.ReversePolicy(policy_code);
            if (output)
            {
                dbar.Action = true;
                dbar.Message = "Revered Successfully.";
            }
            else
            {
                dbar.ErrorMessage = "Error Occured.";
            }
            return dbar;
        }
    }
}
