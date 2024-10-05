using Services.AuthAPI.Dto;

namespace Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto register);
        Task<LoginResponseDto> Login(LoginRequestDto login);
        Task<bool> AssignRole(string email, string role);
    }
}
