using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class GetSessionDto
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public long HallId { get; set; }
    public string? Title { get; set; }
    public DateOnly? Date { get; set; }
    public TimeOnly? Time { get; set; }
    public MovieLang? Language { get; set; }
    public MovieFormat? Format { get; set; }
}
