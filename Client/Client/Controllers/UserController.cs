using Client.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        const string PORT = "7289";
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var endpoint = new Uri($"https://localhost:{PORT}/api/User/LoginUser");
                var loginUser = new LoginUserDTO { Username = loginUserDTO.Username, Password = loginUserDTO.Password };
                var json = JsonConvert.SerializeObject(loginUser);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, payload);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ViewData["error"] = result;
                    return View();
                }
                var dto = JsonConvert.DeserializeObject<LoginUserResponseDTO>(result);
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
                    new Claim(ClaimTypes.Name, dto.Username)
                    };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Blog");
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var endpoint = new Uri($"https://localhost:{PORT}/api/User/RegisterUser");
                var registerUser = new RegisterUserDTO { Username = registerUserDTO.Username, Password = registerUserDTO.Password, FullName=registerUserDTO.FullName, RePassword=registerUserDTO.RePassword };
                var json = JsonConvert.SerializeObject(registerUser);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, payload);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    TempData["error"] = result;
                    return RedirectToAction("Register", "User");
                }
                TempData["success"] = result;
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Blog");
        }
    }
}
