using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Payload for creating or updating a session seat booking.</summary>
public class SaveSessionSeatDto
{
    /// <summary>Identifier of the screening session.</summary>
    [Required]
    public long? SessionId { get; set; }

    /// <summary>Identifier of the seat being booked.</summary>
    [Required]
    public long? SeatId { get; set; }

    /// <summary>Name of the customer making the booking.</summary>
    [Required]
    [MaxLength(255)]
    public string? CustomerName { get; set; }

    /// <summary>Customer contact phone number.</summary>
    [Required]
    [MaxLength(255)]
    public string? Contact { get; set; }

    /// <summary>Operational status of the booking record.</summary>
    public SeatStatus? Status { get; set; }

    /// <summary>Availability flag stored as a string ("true"/"false").</summary>
    [MaxLength(50)]
    public string? IsAvailable { get; set; }
}
