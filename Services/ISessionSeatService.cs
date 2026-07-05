using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing session seat bookings.</summary>
public interface ISessionSeatService
{
    /// <summary>Creates a new session seat booking.</summary>
    /// <exception cref="KeyNotFoundException">Referenced session does not exist.</exception>
    /// <exception cref="KeyNotFoundException">Referenced seat does not exist.</exception>
    Task<GetSessionSeatDto> CreateSessionSeatAsync(SaveSessionSeatDto dto);

    /// <summary>Returns a paginated list of session seat bookings ordered by session and seat.</summary>
    Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeatsAsync(int page, int size);

    /// <summary>Returns a session seat booking by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Session seat does not exist.</exception>
    Task<GetSessionSeatDto> GetSessionSeatAsync(long id);

    /// <summary>Updates an existing session seat booking.</summary>
    /// <exception cref="KeyNotFoundException">Session seat does not exist.</exception>
    /// <exception cref="KeyNotFoundException">Referenced session does not exist.</exception>
    /// <exception cref="KeyNotFoundException">Referenced seat does not exist.</exception>
    Task<GetSessionSeatDto> UpdateSessionSeatAsync(long id, SaveSessionSeatDto dto);

    /// <summary>Deletes a session seat booking.</summary>
    /// <exception cref="KeyNotFoundException">Session seat does not exist.</exception>
    Task DeleteSessionSeatAsync(long id);
}
