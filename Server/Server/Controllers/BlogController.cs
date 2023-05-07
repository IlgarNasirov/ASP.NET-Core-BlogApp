using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.IRepositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        [HttpGet("AllBlogs/{id?}")]
        public async Task<IActionResult> AllBlogs(int? id)
        {
            return Ok(_blogRepository.AllBlogs(id));
        }
        [HttpPost("AddBlog")]
        public async Task<IActionResult>AddBlog(AddBlogDTO addBlogDTO)
        {
            var result = await _blogRepository.AddBlog(addBlogDTO);
            return Ok(result.Message);
        }
        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(AddBlogDTO addBlogDTO)
        {
            var result = await _blogRepository.UpdateBlog(addBlogDTO);
            if (result.Type == false)
            {
                return NotFound("Blog not found!");
            }
            return Ok(result.Message);
        }
        [HttpGet("DetailsBlog/id={id}")]
        public async Task<IActionResult> DetailsBlog(int id)
        {
            var result =await _blogRepository.DetailsBlog(id);
            if (result == null)
            {
                return NotFound("Blog not found!");
            }
            return Ok(result);
        }
        [HttpDelete("DeleteBlog/id={id}&userid={userid}")]
        public async Task<IActionResult> DeleteBlog(int id, int userid)
        {
            var result = await _blogRepository.DeleteBlog(id, userid);
            if (result.Type == false)
                return NotFound("Blog not found!");
            return Ok(result.Message);
        }
        [HttpGet("FillUpdateBlog/id={id}&userid={userid}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult>FillUpdateBlog(int id, int userId)
        {
            var result = await _blogRepository.FillUpdateBlog(id, userId);
            if (result == null)
            {
                return NotFound("Blog not found!");
            }
            return Ok(result);
        }
    }
}
