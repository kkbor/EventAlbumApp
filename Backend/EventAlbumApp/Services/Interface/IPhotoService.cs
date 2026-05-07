using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;

namespace EventAlbumApp.Services.Interface
{
    public interface IPhotoService
    {
        Task<ApiResponse> AddPhotosAsync(DTOUploadPhotos dto);
        //dodanie nowego zdjęcia
        Task<ApiResponse> addPhotoRaw(IFormFile file, Guid albumId);
        //dopasowaie zdjęcia do albumu
        Task<ApiResponse> AdjustPhotosAsync(DTOChangePhotoLocation dto);
    }
}
