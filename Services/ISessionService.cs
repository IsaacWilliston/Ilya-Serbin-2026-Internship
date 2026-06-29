using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing movie screening sessions.</summary>
public interface ISessionService
{
    /// <summary>Creates a new session.</summary>
    Task<GetSessionDto> CreateSessionAsync(SaveSessionDto dto);

    /// <summary>Returns a paginated list of sessions ordered by date and time.</summary>
    Task<PagedResult<GetSessionDto>> GetAllSessionsAsync(int page, int size);

    /// <summary>Returns a session by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Session does not exist.</exception>
    Task<GetSessionDto> GetSessionAsync(long id);

    /// <summary>Updates an existing session.</summary>
    /// <exception cref="KeyNotFoundException">Session does not exist.</exception>
    Task<GetSessionDto> UpdateSessionAsync(long id, SaveSessionDto dto);

    /// <summary>Deletes a session.</summary>
    /// <exception cref="KeyNotFoundException">Session does not exist.</exception>
    Task DeleteSessionAsync(long id);
}
