using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.IRepositories;
using Server.Models;

namespace Server.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly BlogDbContext _db;
        public UserRepository(BlogDbContext db)
        {
            _db = db;
        }
        public async Task<LoginUserResponseDTO> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _db.Users.Where(u => u.Username == loginUserDTO.Username).FirstOrDefaultAsync();
            var errorMessage = "Invalid username or password!";
            if (user == null)
            {
                return new LoginUserResponseDTO { Type = false, Message = errorMessage };
            }
            if (!BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, user.Passwordhash))
            {
                return new LoginUserResponseDTO { Type = false, Message = errorMessage };
            }
            return new LoginUserResponseDTO { Type=true, Id=user.Id, Username=user.Username };
        }
        public async Task<CustomReturnDTO> Register(RegisterUserDTO registerUserDTO)
        {
            var user = await _db.Users.Where(u => u.Username == registerUserDTO.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                var newUser = new User();
                newUser.Passwordhash = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password);
                newUser.Username = registerUserDTO.Username;
                newUser.Fullname = registerUserDTO.FullName;
                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return new CustomReturnDTO { Type = true, Message = "User successfully registered!" };
            }
            return new CustomReturnDTO { Type = false, Message = "This username already exists!" };
        }

    }
}
