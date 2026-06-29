using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

/// <summary>A cinema venue containing one or more screening halls.</summary>
[Table("cinemas")]
public class CinemaEntity
{
    /// <summary>Unique cinema identifier.</summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>Cinema name.</summary>
    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    /// <summary>Street address.</summary>
    [Column("address")]
    [MaxLength(500)]
    public string? Address { get; set; }

    /// <summary>City where the cinema is located.</summary>
    [Column("city")]
    [MaxLength(255)]
    public string? City { get; set; }

    /// <summary>Halls in this cinema.</summary>
    public ICollection<HallEntity> Halls { get; set; } = [];
}
