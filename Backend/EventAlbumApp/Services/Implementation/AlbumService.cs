using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using EventAlbumApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace EventAlbumApp.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly AppdbContext _context;

        public AlbumService(AppdbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> CreateAlbumAsync(DTOalbum dto, string baseUrl, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return ApiResponse<object>.ErrorResponse("Użytkownik nie zalogowany", "USER_NOT_FOUND");

            var qr = new Qr
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            var album = new Album
            {
                Id = Guid.NewGuid(),
                IdUser = userId,
                IdQr = qr.Id,
                Name = dto.Name,
                Start = dto.Start,
                End = dto.End,
                CreatedAt = DateTime.UtcNow
            };

            _context.Qrs.Add(qr);
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            var qrUrl = $"{baseUrl}/api/album/by-qr/{qr.Token}";

            return ApiResponse<object>.SuccessResponse(new
            {
                album.Id,
                album.Name,
                album.Start,
                album.End,
                QrToken = qr.Token,
                QrRedirectUrl = qrUrl
            }, "Album utworzony pomyślnie");
        }

        public async Task<ApiResponse<object>> GetAlbumByQrAsync(Guid token)
        {
            var album = await _context.Albums
                .Include(a => a.Photos)
                .Include(a => a.Qr)
                .FirstOrDefaultAsync(a => a.Qr.Token == token);

            if (album == null)
                return ApiResponse<object>.ErrorResponse("Nie znaleziono albumu", "ALBUM_NOT_FOUND");

            return ApiResponse<object>.SuccessResponse(new
            {
                album.Id,
                album.Name,
                album.Start,
                album.End,
                Photos = album.Photos
            });
        }


        public byte[] GenerateQrImageBytes(Guid token, string baseUrl)
        {
            var url = $"{baseUrl}/api/album/by-qr/{token}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            var qrCode = new PngByteQRCode(qrData);
            var qrBytes = qrCode.GetGraphic(20); 

            return qrBytes;
        }

        public async Task<ApiResponse<IEnumerable<object>>> GetActiveAlbumAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return ApiResponse<IEnumerable<object>>.ErrorResponse("Nieprawidłowy email lub hasło", "USER_NOT_FOUND");
            var now = DateTime.UtcNow;
            var activeAlbums = await _context.Albums
                .Where(a => a.IdUser == userId && a.End > now)
                .Select(a => new
                {   
                    a.Id,
                    a.Name,
                    a.Start,
                    a.End,
                })
                .ToListAsync();

            return ApiResponse<IEnumerable<object>>.SuccessResponse(activeAlbums, "Lista aktywnych albumów użytkownika");
        }

        public async Task<ApiResponse<IEnumerable<object>>> GetEndedAlbumAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return ApiResponse<IEnumerable<object>>.ErrorResponse("Nieprawidłowy email lub hasło", "USER_NOT_FOUND");
            var now = DateTime.UtcNow;
            var activeAlbums = await _context.Albums
                .Where(a => a.IdUser == userId && a.End <= now)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Start,
                    a.End,
                })
                .ToListAsync();

            return ApiResponse<IEnumerable<object>>.SuccessResponse(activeAlbums, "Lista aktywnych albumów użytkownika");
        }
    }
}