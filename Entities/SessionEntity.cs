using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("sessions")]
public class SessionEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("movie_id")]
    public long MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public MovieEntity Movie { get; set; } = null!;

    [Column("hall_id")]
    public long HallId { get; set; }

    [ForeignKey(nameof(HallId))]
    public HallEntity Hall { get; set; } = null!;

    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }

    [Column("time")]
    public TimeOnly? Time { get; set; }

    [Column("language")]
    [MaxLength(50)]
    public MovieLang? Language { get; set; }

    [Column("format")]
    [MaxLength(50)]
    public MovieFormat? Format { get; set; }
}
