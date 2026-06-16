using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class GetSessionSeatDto
{
    public long Id { get; set; }
    public GetSessionDto? Session { get; set; }
    public GetSeatDto? Place { get; set; }
    public SeatStatus? Status { get; set; }
    public string? IsAvailable { get; set; }
    public string? CustomerName { get; set; }
    public string? Contact { get; set; }
}
