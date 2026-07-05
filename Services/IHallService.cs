using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing cinema halls.</summary>
public interface IHallService
{
    /// <summary>Creates a new hall.</summary>
    /// <exception cref="KeyNotFoundException">Referenced cinema does not exist.</exception>
    Task<GetHallDto> CreateHallAsync(SaveHallDto dto);

    /// <summary>Returns a paginated list of halls ordered by name.</summary>
    Task<PagedResult<GetHallDto>> GetAllHallsAsync(int page, int size);

    /// <summary>Returns a hall by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Hall does not exist.</exception>
    Task<GetHallDto> GetHallAsync(long id);

    /// <summary>Updates an existing hall.</summary>
    /// <exception cref="KeyNotFoundException">Hall does not exist.</exception>
    /// <exception cref="KeyNotFoundException">Referenced cinema does not exist.</exception>
    Task<GetHallDto> UpdateHallAsync(long id, SaveHallDto dto);

    /// <summary>Deletes a hall.</summary>
    /// <exception cref="KeyNotFoundException">Hall does not exist.</exception>
    Task DeleteHallAsync(long id);
}
