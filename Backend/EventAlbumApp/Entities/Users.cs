using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("names")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Column("surname")]
        [MaxLength(100)]
        public string Surname { get; set; } = null!;
        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [Column("password_hash")]
        [MaxLength(255)]
        public string password { get; set; } = null!;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public ICollection<Album>? Albums { get; set; }
    }
}
