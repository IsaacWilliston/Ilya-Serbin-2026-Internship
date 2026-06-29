using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Seat data returned by the API.</summary>
public class GetSeatDto
{
    /// <summary>Unique seat identifier.</summary>
    public long Id { get; set; }

    /// <summary>Row number within the hall.</summary>
    public int? Row { get; set; }

    /// <summary>Seat number within the row.</summary>
    public int? Number { get; set; }

    /// <summary>Operational status of the seat.</summary>
    public SeatStatus? Status { get; set; }

    /// <summary>Whether the seat is available for booking.</summary>
    public bool? IsAvailable { get; set; }

    /// <summary>Optional note about the seat (e.g. VIP, broken armrest).</summary>
    public string? Comment { get; set; }
}
