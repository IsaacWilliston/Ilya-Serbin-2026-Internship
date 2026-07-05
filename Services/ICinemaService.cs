using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing cinemas.</summary>
public interface ICinemaService
{
    /// <summary>Creates a new cinema.</summary>
    Task<GetCinemaDto> CreateCinemaAsync(SaveCinemaDto dto);

    /// <summary>Returns a paginated list of cinemas ordered by name.</summary>
    Task<PagedResult<GetCinemaDto>> GetAllCinemasAsync(int page, int size);

    /// <summary>Returns a cinema by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Cinema does not exist.</exception>
    Task<GetCinemaDto> GetCinemaAsync(long id);

    /// <summary>Updates an existing cinema.</summary>
    /// <exception cref="KeyNotFoundException">Cinema does not exist.</exception>
    Task<GetCinemaDto> UpdateCinemaAsync(long id, SaveCinemaDto dto);

    /// <summary>Deletes a cinema.</summary>
    /// <exception cref="KeyNotFoundException">Cinema does not exist.</exception>
    Task DeleteCinemaAsync(long id);
}
