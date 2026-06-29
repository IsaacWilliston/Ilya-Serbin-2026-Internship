using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Screening session data returned by the API.</summary>
public class GetSessionDto
{
    /// <summary>Unique session identifier.</summary>
    public long Id { get; set; }

    /// <summary>Identifier of the movie being shown.</summary>
    public long MovieId { get; set; }

    /// <summary>Identifier of the hall where the session takes place.</summary>
    public long HallId { get; set; }

    /// <summary>Display title for the session.</summary>
    public string? Title { get; set; }

    /// <summary>Date of the screening.</summary>
    public DateOnly? Date { get; set; }

    /// <summary>Start time of the screening.</summary>
    public TimeOnly? Time { get; set; }

    /// <summary>Audio language of the screening.</summary>
    public MovieLang? Language { get; set; }

    /// <summary>Projection format (2D, 3D, IMAX).</summary>
    public MovieFormat? Format { get; set; }
}
