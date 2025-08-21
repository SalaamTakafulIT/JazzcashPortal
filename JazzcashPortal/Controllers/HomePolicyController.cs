using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;

namespace JazzcashPortal.Controllers
{
    public class HomePolicyController : Controller
    {
        private readonly IConfiguration _config;
        private readonly HomePolicyService _BLLS;
        public HomePolicyController(HomePolicyService BLLS, IConfiguration config)
        {
            _BLLS = BLLS;
            _config = config ?? throw new ArgumentNullException(nameof(config));
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

                string? x_client_id = _config["MySettings:X_CLIENT_ID"];
                string? x_client_secret = _config["MySettings:X_CLIENT_SECRET"];
                string? x_partner_id = _config["MySettings:X_PARTNER_ID"];
                string? secret_key = _config["MySettings:Secret_Key"];
                string? iv = _config["MySettings:IV"];

                try
                {
                    // Validate once at startup
                    if (string.IsNullOrWhiteSpace(x_client_id) ||
                        string.IsNullOrWhiteSpace(x_client_secret) ||
                        string.IsNullOrWhiteSpace(x_partner_id) ||
                        string.IsNullOrWhiteSpace(secret_key) ||
                        string.IsNullOrWhiteSpace(iv))
                    {
                        throw new ConfigurationErrorsException("One or more JazzCash configuration values are missing");
                    }
                }
                catch (ConfigurationErrorsException ex)
                {

                    throw;
                }

                var obj = new Jazzcash
                {
                    TRANSACTION_ID = trans_code,
                    REFERENCE_NO = "",
                    X_CLIENT_ID = x_client_id,
                    X_CLIENT_SECRET = x_client_secret,
                    X_PARTNER_ID = x_partner_id,
                    Secret_Key = secret_key,
                    IV = iv
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

        [HttpPost]
        public JsonResult SearchHomePolicy(HomePolicy model)
        {
            try
            {
                var dt = _BLLS.SearchHomePolicy(model);
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
