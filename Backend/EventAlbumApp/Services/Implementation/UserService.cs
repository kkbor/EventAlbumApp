using BCrypt.Net;
using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using EventAlbumApp.Services.Implementation;
using EventAlbumApp.Services.Interface;
using EventAlbumApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventAlbumApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppdbContext _context;
        private readonly IJwtService _jwtService;
        private readonly R2UrlService _r2;
        public UserService(AppdbContext context, IJwtService jwtService,R2UrlService r2)
        {
            _context = context;
            _jwtService = jwtService;
            _r2 = r2;
        }

        public async Task<ApiResponse> RegisterAsync(DTOregister dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return ApiResponse.ErrorResponse("Użytkownik z takim e-mailem już istnieje", "USER_EXISTS");
            }
            Guid userid = Guid.NewGuid();

            var user = new Users
            {
                Id = userid,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(user);
            var qr = new Qr
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            
            var album = new Album
            {
                Id = Guid.NewGuid(),
                IdUser = userid,
                IdQr = qr.Id,
                Name = "%%%",
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow.AddYears(99),
                CreatedAt = DateTime.UtcNow

            };



            _context.Qrs.Add(qr);
            _context.Albums.Add(album);
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
            var albumIds = await _context.Albums
                .Where(a => a.IdUser == id)
                .Select(a => a.Id)
                .ToListAsync();
            var listOfPhotos = await _context.Photos
                .Where(p => albumIds.Contains(p.IdAlbum))
                .Select(p => new DTOPhotos
                {
                    Id = p.Id,
                    IdAlbum = p.IdAlbum,
                    Name = p.Name,
                    Path = p.Path,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
            foreach (var photo in listOfPhotos)
            {
                photo.Path = _r2.GetSignedUrl(photo.Path);
            }
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