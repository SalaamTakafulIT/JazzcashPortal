using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using JazzcashPortal.Models.Setups;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JazzcashPortal.Controllers
{
    public class ProductConfigurationController : Controller
    {
        private readonly ProductConfigurationService _BLLS;
        public ProductConfigurationController(ProductConfigurationService BLLS)
        {
            _BLLS = BLLS;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProductConfig()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.GetProductConfig();
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }

        [HttpGet]
        public IActionResult EditProductConfig(string Id)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.GetProductConfigById(Id);
                var list = DataTableHelper.ToDictionaryList(dt);
                return Json(list);
            }
            catch (Exception)
            {
                return Json(dt);
            }
        }

        [HttpPost]
        public JsonResult SaveProductConfiguration(ProductConfiguration model)
        {
            var dbar = new DbActionResult();
            if (!ModelState.IsValid)
            {
                return Json(new DbActionResult
                {
                    Action = false,
                    ErrorMessage = "Model state is invalid. Please check the input data."
                });
            }

            var result = _BLLS.SaveProductConfiguration(model);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateProductConfiguration(ProductConfiguration model)
        {
            var dbar = new DbActionResult();
            if (!ModelState.IsValid)
            {
                return Json(new DbActionResult
                {
                    Action = false,
                    ErrorMessage = "Model state is invalid. Please check the input data."
                });
            }

            var result = _BLLS.UpdateProductConfiguration(model);
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteProductConfiguration(string Id)
        {
            var dbar = new DbActionResult();
            try
            {
                if (Id == null || Id == "")
                {
                    return Json(new DbActionResult
                    {
                        Action = false,
                        ErrorMessage = "Record Not Found."
                    });
                }

                dbar = _BLLS.DeleteProductConfiguration(Id);
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
