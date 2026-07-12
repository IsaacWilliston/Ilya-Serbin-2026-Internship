using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

public interface IAuthService
{
    Task<GetUserDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}