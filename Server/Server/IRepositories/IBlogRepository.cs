using Server.DTOs;

namespace Server.IRepositories
{
    public interface IBlogRepository
    {
        public IQueryable<AllBlogsDTO> AllBlogs(int? id);
        public Task<CustomReturnDTO> AddBlog(AddBlogDTO addBlogDTO);
        public Task<CustomReturnDTO> UpdateBlog(AddBlogDTO addBlogDTO);
        public Task<CustomReturnDTO> DeleteBlog(int id, int userId);
        public Task<AddBlogDTO> FillUpdateBlog(int id, int userId);
        public Task<DetailsBlogDTO> DetailsBlog(int id);
    }
}
