using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Payload for creating or updating a screening session.</summary>
public class SaveSessionDto
{
    /// <summary>Identifier of the movie being shown.</summary>
    [Required]
    public long? MovieId { get; set; }

    /// <summary>Identifier of the hall where the session takes place.</summary>
    [Required]
    public long? HallId { get; set; }

    /// <summary>Display title for the session.</summary>
    [MaxLength(150)]
    public string? Title { get; set; }

    /// <summary>Date of the screening.</summary>
    [Required]
    public DateOnly? Date { get; set; }

    /// <summary>Start time of the screening.</summary>
    [Required]
    public TimeOnly? Time { get; set; }

    /// <summary>Audio language of the screening.</summary>
    public MovieLang? Language { get; set; }

    /// <summary>Projection format (2D, 3D, IMAX).</summary>
    public MovieFormat? Format { get; set; }
}
