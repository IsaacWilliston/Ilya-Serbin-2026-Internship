using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs;

public class SaveHallDto
{
    /// <summary>Parent cinema identifier.</summary>
    [Required]
    public long? CinemaId {get; set;}
    
    /// <summary>Hall name (e.g. "Hall A").</summary>
    [Required]
    [MaxLength(255)]
    public string? Name {get; set;}
}