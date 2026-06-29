using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

/// <summary>Payload for creating or updating a movie.</summary>
public class SaveMovieDto
{
    /// <summary>Movie title.</summary>
    [MaxLength(150)]
    public string? Title { get; set; }

    /// <summary>Runtime in minutes.</summary>
    [Required]
    public int? DurationMinutes { get; set; }

    /// <summary>Parental guidance rating.</summary>
    public AgeRating? AgeRating { get; set; }

    /// <summary>Average user rating.</summary>
    public float? Rating { get; set; }

    /// <summary>URL of the movie poster image.</summary>
    public string? PosterUrl { get; set; }

    /// <summary>Short plot description.</summary>
    public string? Description { get; set; }

    /// <summary>Genres to associate with the movie.</summary>
    public IEnumerable<Genre> Genres { get; set; } = [];

    /// <summary>Year the movie was released.</summary>
    public int? ReleaseYear { get; set; }
}
