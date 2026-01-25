using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.DTO
{
    public class DTOalbum
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
