using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventAlbumApp.DTO
{
    public class DTOregister
        {
            [Required, MaxLength(100)]
            public string Name { get; set; } = null!;

            [Required, MaxLength(100)]
            public string Surname { get; set; } = null!;

            [Required, EmailAddress]
            public string Email { get; set; } = null!;

            [Required, MinLength(6)]
            public string Password { get; set; } = null!;
        }
    
}
