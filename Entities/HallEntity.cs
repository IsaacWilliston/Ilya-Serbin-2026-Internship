using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

[Table("halls")]
public class HallEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("cinema_id")]
    public long CinemaId { get; set; }

    [ForeignKey(nameof(CinemaId))]
    public CinemaEntity Cinema { get; set; } = null!;

    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    public ICollection<SeatEntity> Seats { get; set; } = [];
    public ICollection<SessionEntity> Sessions { get; set; } = [];
}
