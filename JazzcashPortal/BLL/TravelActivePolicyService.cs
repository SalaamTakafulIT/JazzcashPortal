using JazzcashPortal.DAL;
using JazzcashPortal.Models;
using System.Data;

namespace JazzcashPortal.BLL
{
    public class TravelActivePolicyService
    {
        private readonly TravelActivePolicyRepository _dal;
        public TravelActivePolicyService(TravelActivePolicyRepository dal)
        {
            _dal = dal;
        }

        public DataTable SearchTravelActivePolicy(TravelActivePolicy mdl)
        {
            return _dal.SearchTravelActivePolicy(mdl);
        }
    }
}
