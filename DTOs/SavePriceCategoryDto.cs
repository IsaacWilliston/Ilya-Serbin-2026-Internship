using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Payload for creating or updating a price category.</summary>
public class SavePriceCategoryDto
{
    /// <summary>Category type.</summary>
    [Required]
    public PriceCategory? Type { get; set; }

    /// <summary>Display name (e.g. "VIP Recliner").</summary>
    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    /// <summary>Ticket price for this category.</summary>
    [Required]
    public float? Price { get; set; }
}
