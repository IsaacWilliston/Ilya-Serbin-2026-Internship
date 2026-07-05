using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Price category data returned by the API.</summary>
public class GetPriceCategoryDto
{
    /// <summary>Unique price category identifier.</summary>
    public long Id { get; set; }

    /// <summary>Category type.</summary>
    public PriceCategory? Type { get; set; }

    /// <summary>Display name (e.g. "VIP Recliner").</summary>
    public string? Name { get; set; }

    /// <summary>Ticket price for this category.</summary>
    public float? Price { get; set; }
}
