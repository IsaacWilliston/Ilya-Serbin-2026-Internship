using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for session seat bookings.
/// </summary>
[ApiController]
[Route("session-seats")]
public class SessionSeatController(ISessionSeatService sessionSeatService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new session seat booking.</summary>
    /// <param name="dto">Booking data including session, seat, and customer details.</param>
    /// <returns>The created booking.</returns>
    /// <response code="201">Booking created.</response>
    /// <response code="400">Validation failed.</response>
    /// <response code="404">Referenced session or seat not found.</response>
    [HttpPost]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSessionSeat([FromBody] SaveSessionSeatDto dto)
    {
        var result = await sessionSeatService.CreateSessionSeatAsync(dto);
        return CreatedAtAction(nameof(GetSessionSeat), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all session seat bookings.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetSessionSeatDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeats(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await sessionSeatService.GetAllSessionSeatsAsync(page, size);

    /// <summary>Returns a session seat booking by identifier.</summary>
    /// <param name="id">Session seat identifier.</param>
    /// <response code="404">Session seat not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionSeatDto> GetSessionSeat(long id)
        => await sessionSeatService.GetSessionSeatAsync(id);

    /// <summary>Updates an existing session seat booking.</summary>
    /// <param name="id">Session seat identifier.</param>
    /// <param name="dto">Updated booking data.</param>
    /// <response code="404">Session seat, session, or seat not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionSeatDto> UpdateSessionSeat(long id, [FromBody] SaveSessionSeatDto dto)
        => await sessionSeatService.UpdateSessionSeatAsync(id, dto);

    /// <summary>Deletes a session seat booking.</summary>
    /// <param name="id">Session seat identifier.</param>
    /// <response code="204">Booking deleted.</response>
    /// <response code="404">Session seat not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSessionSeat(long id)
    {
        await sessionSeatService.DeleteSessionSeatAsync(id);
        return NoContent();
    }
}
