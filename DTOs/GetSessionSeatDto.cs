using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Per-session seat booking data returned by the API.</summary>
public class GetSessionSeatDto
{
    /// <summary>Unique session-seat identifier.</summary>
    public long Id { get; set; }

    /// <summary>The screening session.</summary>
    public GetSessionDto? Session { get; set; }

    /// <summary>The physical seat (place).</summary>
    public GetSeatDto? Place { get; set; }

    /// <summary>Operational status of the booking record.</summary>
    public SeatStatus? Status { get; set; }

    /// <summary>Availability flag stored as a string in the database ("true"/"false").</summary>
    public string? IsAvailable { get; set; }

    /// <summary>Name of the customer who booked the seat, if any.</summary>
    public string? CustomerName { get; set; }

    /// <summary>Customer contact phone number.</summary>
    public string? Contact { get; set; }
}
