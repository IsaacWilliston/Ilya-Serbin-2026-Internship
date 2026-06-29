using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>A pricing tier applied to seats (e.g. VIP, standard, economy).</summary>
[Table("price_category")]
public class PriceCategoryEntity
{
    /// <summary>Unique price category identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Category type.</summary>
    [Column("type")]
    [MaxLength(50)]
    public PriceCategory? Type { get; set; }

    /// <summary>Display name (e.g. "VIP Recliner").</summary>
    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    /// <summary>Ticket price for this category.</summary>
    [Column("price")]
    public float? Price { get; set; }
}
