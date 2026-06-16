using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class SaveSessionDto
{
    [Required]
    public long? MovieId { get; set; }

    [Required]
    public long? HallId { get; set; }

    [MaxLength(150)]
    public string? Title { get; set; }

    [Required]
    public DateOnly? Date { get; set; }

    [Required]
    public TimeOnly? Time { get; set; }

    public MovieLang? Language { get; set; }

    public MovieFormat? Format { get; set; }
}
