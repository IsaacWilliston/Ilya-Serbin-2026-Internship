using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for cinemas.
/// </summary>
[ApiController]
[Route("cinemas")]
public class CinemaController(ICinemaService cinemaService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new cinema.</summary>
    /// <param name="dto">Cinema data including name, address, and city.</param>
    /// <returns>The created cinema.</returns>
    /// <response code="201">Cinema created.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCinema([FromBody] SaveCinemaDto dto)
    {
        var result = await cinemaService.CreateCinemaAsync(dto);
        return CreatedAtAction(nameof(GetCinema), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all cinemas.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetCinemaDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetCinemaDto>> GetAllCinemas(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await cinemaService.GetAllCinemasAsync(page, size);

    /// <summary>Returns a cinema by identifier.</summary>
    /// <param name="id">Cinema identifier.</param>
    /// <response code="404">Cinema not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetCinemaDto> GetCinema(long id)
        => await cinemaService.GetCinemaAsync(id);

    /// <summary>Updates an existing cinema.</summary>
    /// <param name="id">Cinema identifier.</param>
    /// <param name="dto">Updated cinema data.</param>
    /// <response code="404">Cinema not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetCinemaDto> UpdateCinema(long id, [FromBody] SaveCinemaDto dto)
        => await cinemaService.UpdateCinemaAsync(id, dto);

    /// <summary>Deletes a cinema.</summary>
    /// <param name="id">Cinema identifier.</param>
    /// <response code="204">Cinema deleted.</response>
    /// <response code="404">Cinema not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCinema(long id)
    {
        await cinemaService.DeleteCinemaAsync(id);
        return NoContent();
    }
}
