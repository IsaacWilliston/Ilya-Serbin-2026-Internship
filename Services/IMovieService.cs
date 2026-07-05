using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing movies.</summary>
public interface IMovieService
{
    /// <summary>Creates a new movie.</summary>
    Task<GetMovieDto> CreateMovieAsync(SaveMovieDto dto);

    /// <summary>Returns a paginated list of movies ordered by title.</summary>
    Task<PagedResult<GetMovieDto>> GetAllMoviesAsync(int page, int size);

    /// <summary>Returns a movie by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Movie does not exist.</exception>
    Task<GetMovieDto> GetMovieAsync(long id);

    /// <summary>Updates an existing movie.</summary>
    /// <exception cref="KeyNotFoundException">Movie does not exist.</exception>
    Task<GetMovieDto> UpdateMovieAsync(long id, SaveMovieDto dto);

    /// <summary>Deletes a movie.</summary>
    /// <exception cref="KeyNotFoundException">Movie does not exist.</exception>
    Task DeleteMovieAsync(long id);
}
