using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.Entities
{
    [Table("Photos")]
    public class Photos
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("idAlbum")]
        public Guid IdAlbum { get; set; }
        [Column("name")]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Column("datas")]
        public byte[]? Data { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(IdAlbum))]
        public Album? Album { get; set; }
    }
}
