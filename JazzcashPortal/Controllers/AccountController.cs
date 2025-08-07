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
        private readonly IConfiguration _configuration;
        public AccountController(AppDbContext context, IConfiguration configuration)
        {
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
        public async Task<IActionResult> LoginPost([FromBody] LoginViewModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                int isAuthenticated = _context.TBL_USERS
                    .Count(e => e.USER_CD == model.Username && e.USER_PASS == model.Password && e.ACTIVE == "Y");

                if (isAuthenticated == 1)
                {
                    //HttpContext.Session.SetString("UserId", dt.Rows[0]["USER_ID"].ToString() ?? "");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        //new Claim(ClaimTypes.Role, "Admin")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Json(new
                    {
                        action = true,
                        status = "Success",
                        message = "Login successful"
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

        public class LoginViewModel
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
    }
}
