using EventAlbumApp.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.DTO
{
    public class DTOPhotos
    {
        public Guid Id { get; set; }
        
        public Guid IdAlbum { get; set; }
      
        public string? Name { get; set; }
       
        public byte[]? Data { get; set; }
   
        public DateTime CreatedAt { get; set; }

    }
}
