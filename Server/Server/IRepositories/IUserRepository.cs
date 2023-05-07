using Server.DTOs;

namespace Server.IRepositories
{
    public interface IUserRepository
    {
        public Task<LoginUserResponseDTO> Login(LoginUserDTO loginUserDTO);
        public Task<CustomReturnDTO> Register(RegisterUserDTO registerUserDTO);
    }
}
