using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs;

public class SaveCinemaDto
{
    /// <summary>Cinema name.</summary>
    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }
    
    /// <summary>Street address.</summary>
    [Required]
    [MaxLength(500)]
    public string? Address { get; set; }
    
    /// <summary>City where the cinema is located.</summary>
    [Required]
    [MaxLength(255)]
    public string? City { get; set; }
}