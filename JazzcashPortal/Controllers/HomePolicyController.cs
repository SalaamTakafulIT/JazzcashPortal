using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    public class HomePolicyController : Controller
    {
        private readonly HomePolicyService _BLLS;
        public HomePolicyController(HomePolicyService BLLS)
        {
            _BLLS = BLLS;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetHomePolicy()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.GetLeads();
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }

        [HttpPost]
        public JsonResult ReversePolicy(string policy_code)
        {
            var dbar = new DbActionResult();
            try
            {
                if (policy_code == null || policy_code == "")
                {
                    return Json(new DbActionResult
                    {
                        Action = false,
                        ErrorMessage = "Record Not Found."
                    });
                }

                dbar = _BLLS.ReversePolicy(policy_code);
                return Json(dbar);
            }
            catch (Exception)
            {
                dbar.ErrorMessage = "Error occurred while deleting record.";
                return Json(dbar);
            }
        }
    }
}
