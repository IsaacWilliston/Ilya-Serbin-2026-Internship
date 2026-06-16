using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class SaveMovieDto
{
    [MaxLength(150)]
    public string? Title { get; set; }

    [Required]
    public int? DurationMinutes { get; set; }

    public AgeRating? AgeRating { get; set; }
    public float? Rating { get; set; }
    public string? PosterUrl { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Genre> Genres { get; set; } = [];
    public int? ReleaseYear { get; set; }
}
