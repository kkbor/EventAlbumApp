using Amazon.S3;
using Amazon.S3.Model;
using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using EventAlbumApp.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EventAlbumApp.Services.Implementation
{
    public class PhotoService : IPhotoService
    {
        private readonly AppdbContext _context;
        private readonly IAmazonS3 _s3;
        private readonly IConfiguration _config;

        public PhotoService(AppdbContext context, IAmazonS3 s3, IConfiguration config)
        {
            _context = context;
            _s3 = s3;
            _config = config;
        }
        public async Task<ApiResponse> AddPhotosAsync(DTOUploadPhotos dto)
        {
            if (dto.Files == null || !dto.Files.Any())
                return ApiResponse.ErrorResponse("Brak plików");

            foreach (var file in dto.Files)
            {
                var result = await addPhotoRaw(file, dto.AlbumId);

                if (!result.Success)
                    return result;
            }

            return ApiResponse.SuccessResponse("Wszystkie zdjęcia dodane");
        }
        public async Task<ApiResponse> addPhotoRaw(IFormFile file, Guid albumId)
        {
            var key = $"albums/{albumId}/{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _config["S3:Bucket"],
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,

                // 🔥 KLUCZ DO R2 FIX
                DisablePayloadSigning = true
            };

            await _s3.PutObjectAsync(request);

            var url = $"{_config["S3:ServiceUrl"]}/{_config["S3:Bucket"]}/{key}";

            var photo = new Photos
            {
                Id = Guid.NewGuid(),
                IdAlbum = albumId,
                Name = file.FileName,
                Path = url,
                CreatedAt = DateTime.UtcNow
            };

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return ApiResponse.SuccessResponse("Zdjęcie zostało dodane.");
        }

        public async Task<ApiResponse> AdjustPhotosAsync(DTOChangePhotoLocation dto)
        {
            var albumExists = await _context.Albums
                .AnyAsync(a => a.Id == dto.AlbumId);

            if (!albumExists)
                return ApiResponse.ErrorResponse("Album nie istnieje");

            var photos = await _context.Photos
                .Where(p => dto.PhotoIds.Contains(p.Id))
                .ToListAsync();

            if (!photos.Any())
                return ApiResponse.ErrorResponse("Brak zdjęć");

            foreach (var photo in photos)
            {
                photo.IdAlbum = dto.AlbumId;
            }

            await _context.SaveChangesAsync();

            return ApiResponse.SuccessResponse("Zdjęcia przeniesione");
        }
    }
}
