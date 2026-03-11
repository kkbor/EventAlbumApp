using EventAlbumApp.Connetion;
using EventAlbumApp.DTO;
using EventAlbumApp.DTO.Response;
using EventAlbumApp.Entities;
using EventAlbumApp.Services.Interfaces;
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
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        /// <summary>
        /// a function that create a new user album, and also qr code with token to this album
        /// </summary>
        /// <param name="dto"> accepts at the starts class album(user id, name, start, end)</param>
        /// <returns>information abaout album</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAlbum(DTOalbum dto)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var response = await _albumService.CreateAlbumAsync(dto, baseUrl);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        /// <summary>
        /// find album base on qr url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("by-qr/{token:guid}")]
        public async Task<IActionResult> GetAlbumByQr(Guid token)
        {
            var response = await _albumService.GetAlbumByQrAsync(token);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("qr-image/{token:guid}")]
        public IActionResult GetQrImage(Guid token)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var imageBytes = _albumService.GenerateQrImageBytes(token, baseUrl);
       
            return File(imageBytes, "image/png");
        }
        [HttpGet("active/user/{userId:guid}")]
        public async Task<IActionResult> GetActiveAlbumsByUser(Guid userId)
        {
            var response = await _albumService.GetActiveAlbumAsync(userId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("ended/user/{userId:guid}")]
        public async Task<IActionResult> GetEndedAlbumsByUser(Guid userId)
        {
            var response = await _albumService.GetEndedAlbumAsync(userId);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
