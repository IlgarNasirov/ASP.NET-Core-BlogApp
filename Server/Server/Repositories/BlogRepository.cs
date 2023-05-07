using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.IRepositories;
using Server.Models;

namespace Server.Repositories
{
    public class BlogRepository:IBlogRepository
    {
        private readonly BlogDbContext _db;
        public BlogRepository(BlogDbContext db)
        {
            _db = db;
        }
        public IQueryable<AllBlogsDTO> AllBlogs(int? id)
        {
            return from u in _db.Users
                   join b in _db.Blogs
                   on u.Id equals b.Userid
                   where b.Status==true
                   orderby b.Date descending
                   select new AllBlogsDTO
                   {
                       Data = b.Data,
                       Date = DateOnly.FromDateTime(b.Date),
                       Id = b.Id,
                       Title = b.Title,
                       Username = u.Username,
                       IsYour = id == u.Id ? true : false
                   };
        }
        public async Task<CustomReturnDTO>AddBlog(AddBlogDTO addBlogDTO)
        {
            Blog blog = new Blog();
            blog.Title = addBlogDTO.Title;
            blog.Date = DateTime.Now;
            blog.Data = addBlogDTO.Data;
            blog.Userid = addBlogDTO.UserId;
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
            return new CustomReturnDTO { Message = "Blog added successfully!" };
        }
        public async Task<CustomReturnDTO> UpdateBlog(AddBlogDTO addBlogDTO)
        {
            var blog = await _db.Blogs.Where(b => b.Id == addBlogDTO.Id && b.Userid == addBlogDTO.UserId && b.Status==true).FirstOrDefaultAsync();
            if (blog == null)
            {
                return new CustomReturnDTO { Type = false };
            }
            blog.Title = addBlogDTO.Title;
            blog.Date = DateTime.Now;
            blog.Data = addBlogDTO.Data;   
            await _db.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "Blog updated successfully!" };
        }
        public async Task<CustomReturnDTO>DeleteBlog(int id, int userId)
        {
            var blog = await _db.Blogs.Where(b => b.Id == id && b.Userid == userId && b.Status==true).FirstOrDefaultAsync();
            if (blog == null)
            {
                return new CustomReturnDTO { Type = false };
            }
            blog.Status = false;
            await _db.SaveChangesAsync();
            return new CustomReturnDTO { Type=true, Message= "Blog deleted successfully!" };
        }
        public async Task<AddBlogDTO> FillUpdateBlog(int id, int userId)
        {
            var blog = await _db.Blogs.Where(b => b.Id == id && b.Userid == userId && b.Status==true).FirstOrDefaultAsync();
            if (blog == null)
            {
                return null;
            }
            return new AddBlogDTO { Id = id, Data=blog.Data, Title=blog.Title};
        }
        public async Task<DetailsBlogDTO>DetailsBlog(int id)
        {
            var blog = await _db.Blogs.Where(b => b.Id == id && b.Status==true).FirstOrDefaultAsync();
            if (blog == null)
            {
                return null;
            }
            blog.Views++;
            await _db.SaveChangesAsync();
            var userName = (await _db.Users.FindAsync(blog.Userid)).Username;
            return new DetailsBlogDTO { Data = blog.Data, Date = DateOnly.FromDateTime(blog.Date), Title = blog.Title, Views =blog.Views, Username = userName };
        }
    }
}