using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("sessions")]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSession([FromBody] SaveSessionDto dto)
    {
        var result = await sessionService.CreateSessionAsync(dto);
        return CreatedAtAction(nameof(GetSession), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetSessionDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetSessionDto>> GetAllSessions(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await sessionService.GetAllSessionsAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionDto> GetSession(long id)
        => await sessionService.GetSessionAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionDto> UpdateSession(long id, [FromBody] SaveSessionDto dto)
        => await sessionService.UpdateSessionAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSession(long id)
    {
        await sessionService.DeleteSessionAsync(id);
        return NoContent();
    }
}
