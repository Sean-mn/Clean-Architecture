using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                var user = await _service.AuthService!.RegisterAsync(dto);
                
                var token = _service.TokenService!.GenerateToken(user);
                
                return Ok(new
                {
                    Message = "회원가입 성공",
                    UserId = user.Id,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var user = await _service.AuthService.LoginAsync(dto);
                if (user == null)
                    return Unauthorized(new { Message = "잘못된 사용자 이름 또는 비밀번호입니다." });

                var token = _service.TokenService.GenerateToken(user);
                
                return Ok(new
                {
                    Message = "로그인 성공",
                    UserId = user.Id,
                    Username = user.Username,
                    Token = token
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { Message = "서버 오류가 발생했습니다." });
            }
        }
    }
}