using Client.DTOs;
using Client.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Client.Controllers
{
    public class BlogController : Controller
    {
        private readonly IGetUserService _getUserService;
        const string PORT="7289";
        public BlogController(IGetUserService getUserService)
        {
            _getUserService = getUserService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            var client = new HttpClient();
            string uri = $"https://localhost:{PORT}/api/Blog/AllBlogs/";
            if (_getUserService.GetUserId() > 0)
            {
                uri += _getUserService.GetUserId().ToString();
            }
            var endpoint = new Uri(uri);
            var response = await client.GetAsync(endpoint);
            var result = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<List<AllBlogsDTO>>(result);
            return View(dto);
        }
        public IActionResult AddBlog()
        {
            ViewData["title"] = "Add blog";
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBlog(AddBlogDTO addBlogDTO)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var endpoint = new Uri($"https://localhost:{PORT}/api/Blog/AddBlog");
                var addBlog = new AddBlogDTO { Data=addBlogDTO.Data, Title=addBlogDTO.Title, UserId=_getUserService.GetUserId()};
                var json = JsonConvert.SerializeObject(addBlog);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var message = await client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync();
                TempData["success"] = message;
                return RedirectToAction("Index", "Blog");
            }
            ViewData["title"] = "Add blog";
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View();
        }
        public async Task<IActionResult> UpdateBlog(int id)
        {
            var client = new HttpClient();
            var endpoint = new Uri($"https://localhost:{PORT}/api/Blog/FillUpdateBlog/id={id}&userid={_getUserService.GetUserId()}");
            var response = await client.GetAsync(endpoint);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }
            ViewData["title"] = "Update blog";
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View("AddBlog", JsonConvert.DeserializeObject<AddBlogDTO>(result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBlog(AddBlogDTO addBlogDTO)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var endpoint = new Uri($"https://localhost:{PORT}/api/Blog/UpdateBlog");
                var updateBlog = new AddBlogDTO {Id=addBlogDTO.Id, Data = addBlogDTO.Data, Title = addBlogDTO.Title, UserId = _getUserService.GetUserId() };
                var json = JsonConvert.SerializeObject(updateBlog);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(endpoint, payload);
                var message = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = message;
                }
                return RedirectToAction("Index", "Blog");
            }
            ViewData["title"] = "Update blog";
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View("AddBlog");
        }
        [AllowAnonymous]
        public async Task<IActionResult> DetailsBlog(int id)
        {
            var client = new HttpClient();
            var endpoint = new Uri($"https://localhost:{PORT}/api/Blog/DetailsBlog/id={id}");
            var response = await client.GetAsync(endpoint);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }
            if (_getUserService.GetUserId() > 0)
            {
                ViewData["allow"] = "yes";
                ViewData["username"] = _getUserService.GetUsername();
            }
            return View(JsonConvert.DeserializeObject<DetailsBlogDTO>(result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteBlog(int id)
        {
            var client = new HttpClient();
            var endpoint = new Uri($"https://localhost:{PORT}/api/Blog/DeleteBlog/id={id}&userid={_getUserService.GetUserId()}");
            var response = await client.DeleteAsync(endpoint);
            var message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = message;
            }
            return RedirectToAction("Index", "Blog");
        }
    }
}
