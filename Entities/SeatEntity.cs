using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>A physical seat within a cinema hall.</summary>
[Table("seats")]
public class SeatEntity
{
    /// <summary>Unique seat identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Parent hall identifier.</summary>
    [Column("hall_id")]
    public long HallId { get; set; }

    /// <summary>Parent hall.</summary>
    [ForeignKey(nameof(HallId))]
    public HallEntity Hall { get; set; } = null!;

    /// <summary>Price category identifier.</summary>
    [Column("price_category_id")]
    public long PriceCategoryId { get; set; }

    /// <summary>Price category for this seat.</summary>
    [ForeignKey(nameof(PriceCategoryId))]
    public PriceCategoryEntity PriceCategory { get; set; } = null!;

    /// <summary>Row number within the hall.</summary>
    [Column("row")]
    public int? Row { get; set; }

    /// <summary>Seat number within the row.</summary>
    [Column("number")]
    public int? Number { get; set; }

    /// <summary>Operational status of the seat.</summary>
    [Column("status")]
    [MaxLength(50)]
    public SeatStatus? Status { get; set; }

    /// <summary>Whether the seat is available for booking.</summary>
    [Column("is_available")]
    public bool? IsAvailable { get; set; }

    /// <summary>Optional note about the seat.</summary>
    [Column("comment")]
    [MaxLength(500)]
    public string? Comment { get; set; }
}
