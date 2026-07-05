using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for movies.
/// </summary>
[ApiController]
[Route("movies")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new movie.</summary>
    /// <param name="dto">Movie data including title, duration, and genres.</param>
    /// <returns>The created movie.</returns>
    /// <response code="201">Movie created.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromBody] SaveMovieDto dto)
    {
        var result = await movieService.CreateMovieAsync(dto);
        return CreatedAtAction(nameof(GetMovie), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all movies.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetMovieDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetMovieDto>> GetAllMovies(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await movieService.GetAllMoviesAsync(page, size);

    /// <summary>Returns a movie by identifier.</summary>
    /// <param name="id">Movie identifier.</param>
    /// <response code="404">Movie not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetMovieDto> GetMovie(long id)
        => await movieService.GetMovieAsync(id);

    /// <summary>Updates an existing movie.</summary>
    /// <param name="id">Movie identifier.</param>
    /// <param name="dto">Updated movie data.</param>
    /// <response code="404">Movie not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetMovieDto> UpdateMovie(long id, [FromBody] SaveMovieDto dto)
        => await movieService.UpdateMovieAsync(id, dto);

    /// <summary>Deletes a movie.</summary>
    /// <param name="id">Movie identifier.</param>
    /// <response code="204">Movie deleted.</response>
    /// <response code="404">Movie not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMovie(long id)
    {
        await movieService.DeleteMovieAsync(id);
        return NoContent();
    }
}
