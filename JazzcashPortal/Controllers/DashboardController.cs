using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DashboardController : Controller
    {
        private readonly DashboardService _BLLS;
        public DashboardController(DashboardService BLLS)
        {
            _BLLS = BLLS;
        }

        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult GetDashboardData()
        {
            DataTable dt = new DataTable();
            try 
            {
                dt = _BLLS.GetDashboardData();
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }
    }
}
