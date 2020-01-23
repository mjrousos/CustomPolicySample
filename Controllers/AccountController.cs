using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomPolicyProvider.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet]
        public ActionResult Signin(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signin(string userName, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest("A user name is required");

            // In a real-world application, user credentials would need validated before signing in
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));

            // Create user's identity and sign them in
            var identity = new ClaimsIdentity(claims, "UserSpecified");
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return Redirect(returnUrl ?? "/");
        }

        public async Task<ActionResult> Signout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public ActionResult Denied()
        {
            return View();
        }
    }
}
