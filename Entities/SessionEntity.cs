using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

/// <summary>A scheduled movie screening in a hall.</summary>
[Table("sessions")]
public class SessionEntity
{
    /// <summary>Unique session identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Movie being shown.</summary>
    [Column("movie_id")]
    public long MovieId { get; set; }

    /// <summary>Movie navigation property.</summary>
    [ForeignKey(nameof(MovieId))]
    public MovieEntity Movie { get; set; } = null!;

    /// <summary>Hall where the session takes place.</summary>
    [Column("hall_id")]
    public long HallId { get; set; }

    /// <summary>Hall navigation property.</summary>
    [ForeignKey(nameof(HallId))]
    public HallEntity Hall { get; set; } = null!;

    /// <summary>Display title for the session.</summary>
    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    /// <summary>Date of the screening.</summary>
    [Column("date")]
    public DateOnly? Date { get; set; }

    /// <summary>Start time of the screening.</summary>
    [Column("time")]
    public TimeOnly? Time { get; set; }

    /// <summary>Audio language of the screening.</summary>
    [Column("language")]
    [MaxLength(50)]
    public MovieLang? Language { get; set; }

    /// <summary>Projection format (2D, 3D, IMAX).</summary>
    [Column("format")]
    [MaxLength(50)]
    public MovieFormat? Format { get; set; }
}
