using JazzcashPortal.BLL;
using JazzcashPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using static Azure.Core.HttpHeader;

namespace JazzcashPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AccountService _BLLS;
        private readonly IConfiguration _configuration;
        public AccountController(AccountService BLLS, AppDbContext context, IConfiguration configuration)
        {
            _BLLS = BLLS;
            _context = context;
            _configuration = configuration;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginPost([FromBody] Account model)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _BLLS.JazzcashValidate(model);
                //int isAuthenticated = _context.TBL_USERS
                //    .Count(e => e.USER_CD == model.Username && e.USER_PASS == model.Password && e.ACTIVE == "Y");

                if (dt.Rows.Count > 0)
                {
                    string? role = dt.Rows[0]["JAZZCASH_USER_TYPE"].ToString();
                    HttpContext.Session.SetString("UserType", dt.Rows[0]["USER_TYPE"].ToString() ?? "");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, role??"")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Json(new
                    {
                        action = true,
                        status = "Success",
                        message = "Login successful",
                        Role = role,
                    });
                }
                else
                {
                    return Json(new
                    {
                        action = false,
                        status = "Error",
                        message = "Invalid username or password."
                    });
                }

            }
            catch (Exception)
            {
                return Json(new
                {
                    action = false,
                    status = "Error",
                    message = "An error occurred during login. Please try again."
                });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        //public class LoginViewModel
        //{
        //    public required string Username { get; set; }
        //    public required string Password { get; set; }
        //    public string? JAZZCASH_USER_TYPE { get; set; }
        //}
    }
}
