using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs;

public class GetUserDto
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
}