using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("price_category")]
public class PriceCategoryEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("type")]
    [MaxLength(50)]
    public PriceCategory? Type { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    [Column("price")]
    public float? Price { get; set; }
}
