using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;

namespace EventAlbumApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse> RegisterAsync(DTOregister dto);
        Task<ApiResponse<UserResponse>> LoginAsync(DTOLogin dto);
        Task<ApiResponse<DTOUser>> UserInfo(Guid id);
    }
}
