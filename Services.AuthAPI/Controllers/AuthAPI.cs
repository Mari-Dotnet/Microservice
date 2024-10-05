using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AuthAPI.Models.Dto;
using Services.AuthAPI.Dto;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPI : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;

        public AuthAPI(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSucessfull = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            _response.IsSucessfull = true;
            _response.Message = errorMessage;
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto login)
        {
            var user=await _authService.Login(login);
            if (user != null)
            {
                _response.IsSucessfull = true;
                _response.Message = "sucess";
                _response.Result = user;
                return Ok(_response);
            }
            else
            {
                _response.IsSucessfull = false;
                _response.Message = "UserName and Password incorrect";
                _response.Result = new LoginResponseDto();
                return NotFound(_response);
            }
        }

       [HttpPost]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            var rolecreated = await _authService.AssignRole(model.Email, model.Role);
            if (rolecreated)
            {
                _response.IsSucessfull = true;
                _response.Message = "Roles Assigned sucessfull";
                return Ok(_response);
            }
            else
            {
                _response.IsSucessfull = false;
                _response.Message = "Role not mapped on user";
                return NotFound(_response);
            }
        }

    }
}

