namespace EventAlbumApp.DTO
{
    public class DTOUploadPhotos
    {
       
        public Guid AlbumId { get; set; }

        public List<IFormFile> Files { get; set; } = new();
        
    }
}
