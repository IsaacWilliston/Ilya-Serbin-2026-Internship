using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing cinema hall seats.</summary>
public interface ISeatService
{
    /// <summary>Creates a new seat.</summary>
    Task<GetSeatDto> CreatePlaceAsync(SaveSeatDto dto);

    /// <summary>Returns a paginated list of seats ordered by row and number.</summary>
    Task<PagedResult<GetSeatDto>> GetAllPlacesAsync(int page, int size);

    /// <summary>Returns a seat by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Seat does not exist.</exception>
    Task<GetSeatDto> GetPlaceAsync(long id);

    /// <summary>Updates an existing seat.</summary>
    /// <exception cref="KeyNotFoundException">Seat does not exist.</exception>
    Task<GetSeatDto> UpdatePlaceAsync(long id, SaveSeatDto dto);

    /// <summary>Deletes a seat.</summary>
    /// <exception cref="KeyNotFoundException">Seat does not exist.</exception>
    Task DeletePlaceAsync(long id);
}
