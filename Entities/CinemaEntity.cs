using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

[Table("cinemas")]
public class CinemaEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    [Column("address")]
    [MaxLength(500)]
    public string? Address { get; set; }

    [Column("city")]
    [MaxLength(255)]
    public string? City { get; set; }

    public ICollection<HallEntity> Halls { get; set; } = [];
}
