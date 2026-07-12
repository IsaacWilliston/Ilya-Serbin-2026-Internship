using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for cinema halls.
/// </summary>
[ApiController]
[Route("halls")]
[Authorize(Roles = "ADMIN")]
public class HallController(IHallService hallService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new hall in a cinema.</summary>
    /// <param name="dto">Hall data including parent cinema and name.</param>
    /// <returns>The created hall.</returns>
    /// <response code="201">Hall created.</response>
    /// <response code="400">Validation failed.</response>
    /// <response code="404">Referenced cinema not found.</response>
    [HttpPost]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateHall([FromBody] SaveHallDto dto)
    {
        var result = await hallService.CreateHallAsync(dto);
        return CreatedAtAction(nameof(GetHall), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all halls.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetHallDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetHallDto>> GetAllHalls(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await hallService.GetAllHallsAsync(page, size);

    /// <summary>Returns a hall by identifier.</summary>
    /// <param name="id">Hall identifier.</param>
    /// <response code="404">Hall not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetHallDto> GetHall(long id)
        => await hallService.GetHallAsync(id);

    /// <summary>Updates an existing hall.</summary>
    /// <param name="id">Hall identifier.</param>
    /// <param name="dto">Updated hall data.</param>
    /// <response code="404">Hall or referenced cinema not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetHallDto> UpdateHall(long id, [FromBody] SaveHallDto dto)
        => await hallService.UpdateHallAsync(id, dto);

    /// <summary>Deletes a hall.</summary>
    /// <param name="id">Hall identifier.</param>
    /// <response code="204">Hall deleted.</response>
    /// <response code="404">Hall not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHall(long id)
    {
        await hallService.DeleteHallAsync(id);
        return NoContent();
    }
}
