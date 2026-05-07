namespace EventAlbumApp.DTO
{
    public class DTOChangePhotoLocation
    {
     
        public Guid AlbumId { get; set; }

        public List<Guid> PhotoIds { get; set; } = new();
        
    }
}
