using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                if (_service.AuthService == null)
                    return BadRequest(new { Message = "AuthService가 초기화 되지 않았습니다" });
                
                var user = await _service.AuthService.RegisterAsync(dto);
                
                return Ok(new
                {
                    Message = "회원가입 성공",
                    UserId = user.Id
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
                if (_service.AuthService == null)
                    return BadRequest(new { Message = "AuthService가 초기화 되지 않았습니다." });

                var user = await _service.AuthService.LoginAsync(dto);

                if (user == null)
                    return Unauthorized("로그인 실패");

                return Ok(new
                {
                    Message = "로그인 성공",
                    UserId = user.Id,
                    Username = user.Username
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message});
            }
        }
    }
}
