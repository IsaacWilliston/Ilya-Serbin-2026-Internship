using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for cinema hall seats (places).
/// </summary>
[ApiController]
[Route("places")]
public class SeatController(ISeatService seatService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new seat in a hall.</summary>
    /// <param name="dto">Seat data including hall and price category.</param>
    /// <returns>The created seat.</returns>
    /// <response code="201">Seat created.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlace([FromBody] SaveSeatDto dto)
    {
        var result = await seatService.CreatePlaceAsync(dto);
        return CreatedAtAction(nameof(GetPlace), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all seats.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetSeatDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetSeatDto>> GetAllPlaces(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await seatService.GetAllPlacesAsync(page, size);

    /// <summary>Returns a seat by identifier.</summary>
    /// <param name="id">Seat identifier.</param>
    /// <response code="404">Seat not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSeatDto> GetPlace(long id)
        => await seatService.GetPlaceAsync(id);

    /// <summary>Updates an existing seat.</summary>
    /// <param name="id">Seat identifier.</param>
    /// <param name="dto">Updated seat data.</param>
    /// <response code="404">Seat not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSeatDto> UpdatePlace(long id, [FromBody] SaveSeatDto dto)
        => await seatService.UpdatePlaceAsync(id, dto);

    /// <summary>Deletes a seat.</summary>
    /// <param name="id">Seat identifier.</param>
    /// <response code="204">Seat deleted.</response>
    /// <response code="404">Seat not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlace(long id)
    {
        await seatService.DeletePlaceAsync(id);
        return NoContent();
    }
}
