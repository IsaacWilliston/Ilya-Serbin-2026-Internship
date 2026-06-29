using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Movie data returned by the API.</summary>
public class GetMovieDto
{
    /// <summary>Unique movie identifier.</summary>
    public long Id { get; set; }

    /// <summary>Movie title.</summary>
    public string? Title { get; set; }

    /// <summary>Runtime in minutes.</summary>
    public int? DurationMinutes { get; set; }

    /// <summary>Parental guidance rating.</summary>
    public AgeRating? AgeRating { get; set; }

    /// <summary>Average user rating.</summary>
    public float? Rating { get; set; }

    /// <summary>URL of the movie poster image.</summary>
    public string? PosterUrl { get; set; }

    /// <summary>Short plot description.</summary>
    public string? Description { get; set; }

    /// <summary>Genres associated with the movie.</summary>
    public IEnumerable<Genre> Genres { get; set; } = [];

    /// <summary>Year the movie was released.</summary>
    public int? ReleaseYear { get; set; }
}
