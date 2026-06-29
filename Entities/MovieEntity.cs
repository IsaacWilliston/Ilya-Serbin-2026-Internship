using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>A movie available for screening.</summary>
[Table("movies")]
public class MovieEntity
{
    /// <summary>Unique movie identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Movie title.</summary>
    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    /// <summary>Runtime in minutes.</summary>
    [Column("duration-minutes")]
    public int? DurationMinutes { get; set; }

    /// <summary>Parental guidance rating.</summary>
    [Column("age-rating")]
    [MaxLength(50)]
    public AgeRating? AgeRating { get; set; }

    /// <summary>Average user rating.</summary>
    [Column("rating")]
    public float? Rating { get; set; }

    /// <summary>URL of the movie poster image.</summary>
    [Column("poster-url")]
    [MaxLength(1000)]
    public string? PosterUrl { get; set; }

    /// <summary>Short plot description.</summary>
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>Year the movie was released.</summary>
    [Column("release-year")]
    public int? ReleaseYear { get; set; }

    /// <summary>Genre associations for this movie.</summary>
    public ICollection<MovieGenre> Genres { get; set; } = [];
}
