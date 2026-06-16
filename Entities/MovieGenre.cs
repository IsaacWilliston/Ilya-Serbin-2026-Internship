using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("movie_genres")]
public class MovieGenre
{
    [Column("movie_id")]
    public long MovieId { get; set; }

    [Column("genre")]
    [MaxLength(50)]
    public Genre Genre { get; set; }

    [ForeignKey(nameof(MovieId))]
    public MovieEntity Movie { get; set; } = null!;
}
