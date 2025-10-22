using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    [Authorize(Roles = "AGENT,ADMIN")]
    public class TravelActivePolicyController : Controller
    {
        private readonly IConfiguration _config;
        private readonly TravelActivePolicyService _BLLS;
        public TravelActivePolicyController(TravelActivePolicyService BLLS, IConfiguration config)
        {
            _BLLS = BLLS;
            _config = config;
        }
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            var model = new TravelActivePolicy
            {
                PERIOD_FROM = firstDayOfMonth.ToString("dd-MMM-yyyy"),
                PERIOD_TO = today.ToString("dd-MMM-yyyy"),
            };

            ViewBag.ShowDiv = HttpContext.Session.GetString("UserType");

            return View(model);
        }

        [HttpPost]
        public JsonResult SearchTravelActivePolicy(TravelActivePolicy model)
        {
            try
            {
                var dt = _BLLS.SearchTravelActivePolicy(model);
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