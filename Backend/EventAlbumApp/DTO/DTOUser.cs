using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.DTO
{
    public class DTOUser
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Email { get; set; } = null!;
        public List<DTOPhotos>? photos { get; set; }

    }
}
