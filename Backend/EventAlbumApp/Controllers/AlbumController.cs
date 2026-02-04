using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing.Imaging;

namespace EventAlbumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AppdbContext _context;
        public AlbumController(AppdbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// a function that create a new user album, and also qr code with token to this album
        /// </summary>
        /// <param name="dto"> accepts at the starts class album(user id, name, start, end)</param>
        /// <returns>information abaout album</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAlbum(DTOalbum dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                return Unauthorized(
                    ApiResponse.ErrorResponse(
                        "Nieprawidłowy email lub hasło",
                        errorCode: "USER_NOT_FOUND"
                    )
                );
            }
            var qr = new Qr
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            var album = new Album
            {
                Id = Guid.NewGuid(),
                IdUser = dto.UserId,
                IdQr = qr.Id,
                Name = dto.Name,
                Start = dto.Start,
                End = dto.End,
                CreatedAt = DateTime.UtcNow
            };
            _context.Qrs.Add(qr);
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            var qrUrl = $"{Request.Scheme}://{Request.Host}/api/album/by-qr/{qr.Token}";

            return Ok(ApiResponse<object>.SuccessResponse(new
            {
                album.Id,
                album.Name,
                album.Start,
                album.End,
                QrToken = qr.Token,
                QrRedirectUrl = qrUrl
            }, "Album utworzony pomyślnie"));
        }
        /// <summary>
        /// find album base on qr url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("by-qr/{token:guid}")]
        public async Task<IActionResult> GetAlbumByQr(Guid token)
        {
            var album = await _context.Albums
                .Include(a => a.Photos)
                .Include(a => a.Qr)
                .FirstOrDefaultAsync(a => a.Qr.Token == token);

            if (album == null)
            {
                return NotFound(ApiResponse.ErrorResponse(
                    "Nie znaleziono albumu",
                    "ALBUM_NOT_FOUND"
                ));
            }

            return Ok(ApiResponse<object>.SuccessResponse(new
            {
                album.Id,
                album.Name,
                album.Start,
                album.End,
                Photos = album.Photos
            }));
        }
        [HttpGet("qr-image/{token:guid}")]
        public IActionResult GetQrImage(Guid token)
        {
            var url = $"{Request.Scheme}://{Request.Host}/api/album/by-qr/{token}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var bitmap = qrCode.GetGraphic(20);

            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
        }
    }
}
