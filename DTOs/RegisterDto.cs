using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public string Password { get; set; } = null!;
}