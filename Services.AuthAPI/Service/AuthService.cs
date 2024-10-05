using Microsoft.AspNetCore.Identity;
using Service.AuthAPI.Data;
using Services.AuthAPI.Dto;
using Services.AuthAPI.Models;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IJWTTokenGenerator _jWTTokenGenerator;
        public AuthService(AppDbContext db,
                           UserManager<ApplicationUser> UserManager,
                           RoleManager<IdentityRole> Rolemanager,
                           IJWTTokenGenerator jWTTokenGenerator)
        {
            _db = db;
            _userManager = UserManager;
            _rolemanager = Rolemanager;
            _jWTTokenGenerator = jWTTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower().Equals(email));
            if (user is not null)
            {
                if (!_rolemanager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _rolemanager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRolesAsync(user, new List<string> { role });
                return true;

            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto login)
        {
            try
            {
                var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower().Equals(login.UserName.ToLower()));
                bool isvalid = await _userManager.CheckPasswordAsync(user, login.Password);
                if (user is null || !isvalid)
                {
                    return new LoginResponseDto() { user = null, Token = "" };
                }
                else
                {
                    UserDto userdto = new UserDto()
                    {
                        Email = user.Email,
                        PhoneNUmber = user.PhoneNumber,
                        Id = user.Id,
                        Name = user.Name,
                    };
                    string token = _jWTTokenGenerator.GenerateToken(user);
                    LoginResponseDto loginResponse = new LoginResponseDto() { user = userdto, Token = token };
                    return loginResponse;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> Register(RegisterationRequestDto register)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = register.Email,
                Email = register.Email,
                NormalizedEmail = register.Email.ToUpper(),
                Name = register.Name,
                PhoneNumber = register.PhoneNUmber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(predicate: x => x.UserName.Equals(register.Email));
                    if (userToReturn is not null)
                    {
                        UserDto userDto = new UserDto()
                        {
                            Name = userToReturn.Name,
                            Id = userToReturn.Id,
                            Email = userToReturn.Email,
                            PhoneNUmber = userToReturn.PhoneNumber
                        };
                    }
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
    }
}
