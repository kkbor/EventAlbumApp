using EventAlbumApp.DTO;
using EventAlbumApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventAlbumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(DTOregister dto)
        {
            var response = await _userService.RegisterAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOLogin dto)
        {
            var response = await _userService.LoginAsync(dto);

            return response.Success ? Ok(response) : Unauthorized(response);
        }
    }
}
