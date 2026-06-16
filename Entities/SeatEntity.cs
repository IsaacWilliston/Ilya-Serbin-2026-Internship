using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("seats")]
public class SeatEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("hall_id")]
    public long HallId { get; set; }

    [ForeignKey(nameof(HallId))]
    public HallEntity Hall { get; set; } = null!;

    [Column("price_category_id")]
    public long PriceCategoryId { get; set; }

    [ForeignKey(nameof(PriceCategoryId))]
    public PriceCategoryEntity PriceCategory { get; set; } = null!;

    [Column("row")]
    public int? Row { get; set; }

    [Column("number")]
    public int? Number { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public SeatStatus? Status { get; set; }

    [Column("is_available")]
    public bool? IsAvailable { get; set; }

    [Column("comment")]
    [MaxLength(500)]
    public string? Comment { get; set; }
}
