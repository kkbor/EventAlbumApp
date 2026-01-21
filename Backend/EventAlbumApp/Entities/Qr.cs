using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.Entities
{
    [Table("Qr")]
    public class Qr
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("Token")]
        public Guid Token { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public Album? Album { get; set; }
    }
}
