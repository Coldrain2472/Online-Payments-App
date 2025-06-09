using Microsoft.AspNetCore.Mvc;
using OnlinePaymentsApp.Services.Interfaces.User;
using OnlinePaymentsApp.Web.Attributes;
using OnlinePaymentsApp.Web.Helpers;
using OnlinePaymentsApp.Web.Models.User;

namespace OnlinePaymentsApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = IdHelper.GetUserId(HttpContext);

            var user = await userService.GetByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Потребителят не е намерен.";
                return RedirectToAction("Login", "Authentication");
            }

            var viewModel = new UserProfileViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Username = user.Username
            };

            return View(viewModel);
        }
    }
}
