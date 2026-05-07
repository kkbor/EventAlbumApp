using EventAlbumApp.DTO;
using EventAlbumApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EventAlbumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        [HttpPost("addPhoto")]
        public async Task<IActionResult> addNewPhoto([FromForm] DTOUploadPhotos dto)
        {
            var response = await _photoService.AddPhotosAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPost("chengePhotoLocation")]
        public async Task<IActionResult> chengePhotoLocation([FromBody] DTOChangePhotoLocation dto)
        {
            var response = await _photoService.AdjustPhotosAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
