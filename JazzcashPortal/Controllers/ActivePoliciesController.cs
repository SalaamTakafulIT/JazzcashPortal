using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    public class ActivePoliciesController : Controller
    {
        private readonly HomePolicyService _BLLS;
        public ActivePoliciesController(HomePolicyService BLLS)
        {
            _BLLS = BLLS;
        }
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var model = new HomePolicy
            {
                PERIOD_FROM = today.ToString("dd-MMM-yyyy"),
                PERIOD_TO = today.ToString("dd-MMM-yyyy"),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetActivePolicies()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.GetActivePolicies();
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }

        [HttpPost]
        public JsonResult SearchActivePolicy(HomePolicy model)
        {
            try
            {
                var dt = _BLLS.SearchActivePolicy(model);
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
