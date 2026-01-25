using BCrypt.Net;
using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAlbumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppdbContext _context;

        public UserController(AppdbContext context)
        {
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(DTOregister dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(
                    ApiResponse.ErrorResponse(
                        "Użytkownik z takim e-mailem już istnieje",
                        errorCode: "USER_EXISTS"
                    )
                );
            }
            var user = new Users
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse.SuccessResponse("Rejestracja zakończona sukcesem"));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOLogin dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized(
                    ApiResponse.ErrorResponse(
                        "Nieprawidłowy email lub hasło",
                        errorCode: "INVALID_CREDENTIALS"
                    )
                );
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isValidPassword)
            {
                return Unauthorized(
                    ApiResponse.ErrorResponse(
                        "Nieprawidłowy email lub hasło",
                        errorCode: "INVALID_CREDENTIALS"
                    )
                );
            }
            var responseDto = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
            return Ok(
                ApiResponse<UserResponse>.SuccessResponse(
                    responseDto,
                    "Zalogowano pomyślnie"
                )
            );
        }
    }
}
