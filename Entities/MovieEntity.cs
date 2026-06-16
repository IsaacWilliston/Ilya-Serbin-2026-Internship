using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("movies")]
public class MovieEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    [Column("duration-minutes")]
    public int? DurationMinutes { get; set; }

    [Column("age-rating")]
    [MaxLength(50)]
    public AgeRating? AgeRating { get; set; }

    [Column("rating")]
    public float? Rating { get; set; }

    [Column("poster-url")]
    [MaxLength(1000)]
    public string? PosterUrl { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("release-year")]
    public int? ReleaseYear { get; set; }

    public ICollection<MovieGenre> Genres { get; set; } = [];
}
