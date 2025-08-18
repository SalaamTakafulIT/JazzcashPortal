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
        public async Task<JsonResult> ReversePolicy(string trans_code, string policy_code)
        {
            var dbar = new DbActionResult();
            try
            {
                if (string.IsNullOrEmpty(trans_code) || string.IsNullOrEmpty(policy_code))
                {
                    return Json(new DbActionResult
                    {
                        Action = false,
                        ErrorMessage = "Record Not Found."
                    });
                }

                var obj = new Jazzcash
                {
                    TRANSACTION_ID = trans_code,
                    REFERENCE_NO = "",
                    X_CLIENT_ID = "",
                    X_CLIENT_SECRET = "",
                    X_PARTNER_ID = "",
                    Secret_Key = "",
                    IV = ""
                };
                // Call the BLL method to reverse the policy
                dbar = await _BLLS.ReversePolicy(obj, policy_code);
                return Json(dbar);
            }
            catch (Exception)
            {
                dbar.ErrorMessage = "Error occurred while reversing the record.";
                return Json(dbar);
            }
        }
    }
}
