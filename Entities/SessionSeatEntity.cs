using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("session_seats")]
public class SessionSeatEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("session_id")]
    public long SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public SessionEntity Session { get; set; } = null!;

    [Column("seat_id")]
    public long SeatId { get; set; }

    [ForeignKey(nameof(SeatId))]
    public SeatEntity Seat { get; set; } = null!;

    [Column("status")]
    [MaxLength(50)]
    public SeatStatus? Status { get; set; }

    // String in original schema (not boolean)
    [Column("is_available")]
    [MaxLength(50)]
    public string? IsAvailable { get; set; }

    [Column("customer_name")]
    [MaxLength(255)]
    public string? CustomerName { get; set; }

    [Column("contact")]
    [MaxLength(255)]
    public string? Contact { get; set; }
}
