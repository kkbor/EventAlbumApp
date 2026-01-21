using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventAlbumApp.Entities
{
    [Table("Album")]
    public class Album
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("idUser")]
        public Guid IdUser { get; set; } 
        [Column("idQr")]
        public Guid IdQr { get; set; }
        [Column("names")]
        [MaxLength(100)]
        public string? Name { get; set; } 
        [Column("startEvent")]
        public DateTime Start { get; set; }
        [Column("endEvent")]
        public DateTime End { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("idUser")]
        public Users? Users { get; set; }
        [ForeignKey("idQr")]
        public Qr? Qr { get; set; }
        
        public ICollection<Photos>? Photos { get; set; }
    }
}
