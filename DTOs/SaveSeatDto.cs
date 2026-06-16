using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class SaveSeatDto
{
    [Required]
    public int? Row { get; set; }

    [Required]
    public int? Number { get; set; }

    public SeatStatus? Status { get; set; }

    [Required]
    public bool? IsAvailable { get; set; }

    public string? Comment { get; set; }

    [Required]
    public long? HallId { get; set; }

    [Required]
    public long? PriceCategoryId { get; set; }
}
