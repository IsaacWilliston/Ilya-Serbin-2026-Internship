using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

public interface ISessionService
{
    Task<GetSessionDto> CreateSessionAsync(SaveSessionDto dto);
    Task<PagedResult<GetSessionDto>> GetAllSessionsAsync(int page, int size);
    Task<GetSessionDto> GetSessionAsync(long id);
    Task<GetSessionDto> UpdateSessionAsync(long id, SaveSessionDto dto);
    Task DeleteSessionAsync(long id);
}
