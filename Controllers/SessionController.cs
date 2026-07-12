using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for movie screening sessions.
/// </summary>
[ApiController]
[Route("sessions")]
[Authorize(Roles = "ADMIN")]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new screening session.</summary>
    /// <param name="dto">Session data including movie, hall, date, and time.</param>
    /// <returns>The created session.</returns>
    /// <response code="201">Session created.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSession([FromBody] SaveSessionDto dto)
    {
        var result = await sessionService.CreateSessionAsync(dto);
        return CreatedAtAction(nameof(GetSession), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all sessions.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetSessionDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetSessionDto>> GetAllSessions(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await sessionService.GetAllSessionsAsync(page, size);

    /// <summary>Returns a session by identifier.</summary>
    /// <param name="id">Session identifier.</param>
    /// <response code="404">Session not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetSessionDto> GetSession(long id)
        => await sessionService.GetSessionAsync(id);

    /// <summary>Updates an existing session.</summary>
    /// <param name="id">Session identifier.</param>
    /// <param name="dto">Updated session data.</param>
    /// <response code="404">Session not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionDto> UpdateSession(long id, [FromBody] SaveSessionDto dto)
        => await sessionService.UpdateSessionAsync(id, dto);

    /// <summary>Deletes a session.</summary>
    /// <param name="id">Session identifier.</param>
    /// <response code="204">Session deleted.</response>
    /// <response code="404">Session not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSession(long id)
    {
        await sessionService.DeleteSessionAsync(id);
        return NoContent();
    }
}
