using Client.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly IGetUserService _getUserService;
        public ErrorController(IGetUserService getUserService)
        {
            _getUserService= getUserService;
        }
        public IActionResult Error404(int code)
        {
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View();
        }
        public IActionResult Error500()
        {
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View();
        }
    }
}
