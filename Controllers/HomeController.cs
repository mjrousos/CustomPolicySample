using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomPolicyProvider.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        public IAuthorizationService AuthorizationService { get; }

        public HomeController(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // View protected with imperative authorization policy
        public async Task<ActionResult> ProjectA()
        {
            var authResult = await AuthorizationService.AuthorizeAsync(HttpContext.User, "A", new ProjectRequirement("A"));
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return View("Project", "A");
        }

        // View protected with custom parameterized authorization policy
        [Authorize(Policy = "ProjectB")]
        [ProjectAuthorize("B")]
        public ActionResult ProjectB()
        {
            return View("Project", "B");
        }
    }
}
