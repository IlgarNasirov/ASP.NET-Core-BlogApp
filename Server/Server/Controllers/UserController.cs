using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.IRepositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var result = await _userRepository.Login(loginUserDTO);
            if (result.Type == false)
            {
                return NotFound(result.Message);
            }
            return Ok(new {id=result.Id, username=result.Username});
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            var result = await _userRepository.Register(registerUserDTO);
            if (result.Type == false)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}