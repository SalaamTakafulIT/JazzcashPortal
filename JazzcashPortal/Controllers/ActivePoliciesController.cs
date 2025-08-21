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
            return View();
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
    }
}
