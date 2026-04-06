using BCrypt.Net;
using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using EventAlbumApp.Services.Interface;
using EventAlbumApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventAlbumApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppdbContext _context;
        private readonly IJwtService _jwtService; 

        public UserService(AppdbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse> RegisterAsync(DTOregister dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return ApiResponse.ErrorResponse("Użytkownik z takim e-mailem już istnieje", "USER_EXISTS");
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

            return ApiResponse.SuccessResponse("Rejestracja zakończona sukcesem");
        }

        public async Task<ApiResponse<UserResponse>> LoginAsync(DTOLogin dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return ApiResponse<UserResponse>.ErrorResponse("Nieprawidłowy email lub hasło", "INVALID_CREDENTIALS");
            }
            var token = _jwtService.GenerateToken(user.Id, user.Email);
            var responseDto = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Token = token
            };

            return ApiResponse<UserResponse>.SuccessResponse(responseDto, "Zalogowano pomyślnie");
        }
        public async Task<ApiResponse<DTOUser>> UserInfo(Guid id)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var listOfPhotos = await _context.Photos
                .Where(p => _context.Albums
                    .Where(a => a.IdUser == id)
                    .Select(a => a.Id)
                    .Contains(p.IdAlbum))
                .Select(p => new DTOPhotos
                {
                    Id = p.Id,
                    IdAlbum = p.IdAlbum,
                    Name = p.Name,
                    Data = p.Data,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            var response = new DTOUser
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                photos = listOfPhotos,
            };
            return ApiResponse<DTOUser>.SuccessResponse(response);
        }

    }
}