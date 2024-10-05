using Services.AuthAPI.Models;

namespace Services.AuthAPI.Service.IService
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser appuser);
    }
}
