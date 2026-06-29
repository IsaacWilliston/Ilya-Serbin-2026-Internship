using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

/// <summary>A screening hall within a cinema.</summary>
[Table("halls")]
public class HallEntity
{
    /// <summary>Unique hall identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Parent cinema identifier.</summary>
    [Column("cinema_id")]
    public long CinemaId { get; set; }

    /// <summary>Parent cinema.</summary>
    [ForeignKey(nameof(CinemaId))]
    public CinemaEntity Cinema { get; set; } = null!;

    /// <summary>Hall name (e.g. "Hall A").</summary>
    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    /// <summary>Seats in this hall.</summary>
    public ICollection<SeatEntity> Seats { get; set; } = [];

    /// <summary>Sessions scheduled in this hall.</summary>
    public ICollection<SessionEntity> Sessions { get; set; } = [];
}
