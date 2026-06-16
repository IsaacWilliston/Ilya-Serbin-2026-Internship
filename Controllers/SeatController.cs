using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("places")]
public class SeatController(ISeatService seatService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlace([FromBody] SaveSeatDto dto)
    {
        var result = await seatService.CreatePlaceAsync(dto);
        return CreatedAtAction(nameof(GetPlace), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetSeatDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetSeatDto>> GetAllPlaces(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await seatService.GetAllPlacesAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSeatDto> GetPlace(long id)
        => await seatService.GetPlaceAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSeatDto> UpdatePlace(long id, [FromBody] SaveSeatDto dto)
        => await seatService.UpdatePlaceAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlace(long id)
    {
        await seatService.DeletePlaceAsync(id);
        return NoContent();
    }
}
