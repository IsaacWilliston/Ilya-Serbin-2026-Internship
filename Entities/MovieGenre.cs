using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>Join record linking a movie to one of its genres.</summary>
[Table("movie_genres")]
public class MovieGenre
{
    /// <summary>Parent movie identifier.</summary>
    [Column("movie_id")]
    public long MovieId { get; set; }

    /// <summary>Genre value (part of composite primary key).</summary>
    [Column("genre")]
    [MaxLength(50)]
    public Genre Genre { get; set; }

    /// <summary>Parent movie.</summary>
    [ForeignKey(nameof(MovieId))]
    public MovieEntity Movie { get; set; } = null!;
}
