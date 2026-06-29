using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Payload for creating or updating a seat.</summary>
public class SaveSeatDto
{
    /// <summary>Row number within the hall.</summary>
    [Required]
    public int? Row { get; set; }

    /// <summary>Seat number within the row.</summary>
    [Required]
    public int? Number { get; set; }

    /// <summary>Operational status of the seat.</summary>
    public SeatStatus? Status { get; set; }

    /// <summary>Whether the seat is available for booking.</summary>
    [Required]
    public bool? IsAvailable { get; set; }

    /// <summary>Optional note about the seat.</summary>
    public string? Comment { get; set; }

    /// <summary>Identifier of the hall this seat belongs to.</summary>
    [Required]
    public long? HallId { get; set; }

    /// <summary>Identifier of the price category for this seat.</summary>
    [Required]
    public long? PriceCategoryId { get; set; }
}
