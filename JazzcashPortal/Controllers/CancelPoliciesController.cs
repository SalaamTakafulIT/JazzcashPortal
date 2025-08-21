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
            return View();
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
    }
}
