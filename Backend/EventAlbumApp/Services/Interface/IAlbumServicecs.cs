using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using System.Drawing;

namespace EventAlbumApp.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<ApiResponse<object>> CreateAlbumAsync(DTOalbum dto, string baseUrl, Guid userId );
        Task<ApiResponse<object>> GetAlbumByQrAsync(Guid token);
        Task<ApiResponse<IEnumerable<object>>> GetActiveAlbumAsync(Guid userId);
        Task<ApiResponse<IEnumerable<object>>> GetEndedAlbumAsync(Guid userId);
        byte[] GenerateQrImageBytes(Guid token, string baseUrl);
    }
}