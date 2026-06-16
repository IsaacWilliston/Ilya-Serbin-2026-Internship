using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class GetSeatDto
{
    public long Id { get; set; }
    public int? Row { get; set; }
    public int? Number { get; set; }
    public SeatStatus? Status { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Comment { get; set; }
}
