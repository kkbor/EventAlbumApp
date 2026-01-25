namespace EventAlbumApp.DTO.Response
{
    public class AlbumResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string QrToken { get; set; } = null!;
        public string QrUrl { get; set; } = null!;
    }
}
