using JazzcashPortal.DAL;
using System.Data;

namespace JazzcashPortal.BLL
{
    public class DashboardService
    {
        private readonly DashboardRepository _dal;
        public DashboardService(DashboardRepository dal)
        {
            _dal = dal;
        }

        public DataTable GetDashboardData()
        {
            return _dal.GetDashboardData();
        }
    }
}
