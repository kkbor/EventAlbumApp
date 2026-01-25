using System.ComponentModel.DataAnnotations;

namespace EventAlbumApp.DTO
{
    public class DTOLogin
    {
       
            [Required, EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
        
    }
}
