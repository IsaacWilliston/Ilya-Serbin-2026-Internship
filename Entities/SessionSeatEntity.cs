using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>
/// Links a seat to a session and optionally records a customer booking.
/// </summary>
[Table("session_seats")]
public class SessionSeatEntity
{
    /// <summary>Unique session-seat identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Parent session identifier.</summary>
    [Column("session_id")]
    public long SessionId { get; set; }

    /// <summary>Parent session.</summary>
    [ForeignKey(nameof(SessionId))]
    public SessionEntity Session { get; set; } = null!;

    /// <summary>Booked seat identifier.</summary>
    [Column("seat_id")]
    public long SeatId { get; set; }

    /// <summary>Booked seat.</summary>
    [ForeignKey(nameof(SeatId))]
    public SeatEntity Seat { get; set; } = null!;

    /// <summary>Operational status of the booking record.</summary>
    [Column("status")]
    [MaxLength(50)]
    public SeatStatus? Status { get; set; }

    /// <summary>Availability flag stored as a string in the original schema ("true"/"false").</summary>
    [Column("is_available")]
    [MaxLength(50)]
    public string? IsAvailable { get; set; }

    /// <summary>Name of the customer who booked the seat, if any.</summary>
    [Column("customer_name")]
    [MaxLength(255)]
    public string? CustomerName { get; set; }

    /// <summary>Customer contact phone number.</summary>
    [Column("contact")]
    [MaxLength(255)]
    public string? Contact { get; set; }
}
