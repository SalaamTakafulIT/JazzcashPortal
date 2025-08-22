using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    public class CancelPoliciesController : Controller
    {
        private readonly HomePolicyService _BLLS;
        public CancelPoliciesController(HomePolicyService BLLS)
        {
            _BLLS = BLLS;
        }
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var firstDateOfMonth = new DateTime(today.Year, today.Month, 1);
            var model = new HomePolicy
            {
                PERIOD_FROM = firstDateOfMonth.ToString("dd-MMM-yyyy"),
                PERIOD_TO = today.ToString("dd-MMM-yyyy"),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetCancelPolicies()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.GetCancelPolicies();
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }

        [HttpPost]
        public JsonResult SearchCancelPolicy(HomePolicy model)
        {
            try
            {
                var dt = _BLLS.SearchCancelPolicy(model);
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(new { success = true, data = list });
            }
            catch (Exception)
            {
                return Json(new { success = false, error = "Error occurred while fetching policy details." });
            }
        }
    }
}
