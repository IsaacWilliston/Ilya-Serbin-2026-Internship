using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Services;

namespace SeatsReservationDotNet.Controllers;

/// <summary>
/// CRUD endpoints for seat price categories.
/// </summary>
[ApiController]
[Route("price-categories")]
public class PriceCategoryController(IPriceCategoryService priceCategoryService) : ControllerBase
{
    private const int DefaultPageSize = 20;

    /// <summary>Creates a new price category.</summary>
    /// <param name="dto">Price category data including type, name, and price.</param>
    /// <returns>The created price category.</returns>
    /// <response code="201">Price category created.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePriceCategory([FromBody] SavePriceCategoryDto dto)
    {
        var result = await priceCategoryService.CreatePriceCategoryAsync(dto);
        return CreatedAtAction(nameof(GetPriceCategory), new { id = result.Id }, result);
    }

    /// <summary>Returns a paginated list of all price categories.</summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Number of items per page.</param>
    [HttpGet]
    [ProducesResponseType<PagedResult<GetPriceCategoryDto>>(StatusCodes.Status200OK)]
    public async Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategories(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await priceCategoryService.GetAllPriceCategoriesAsync(page, size);

    /// <summary>Returns a price category by identifier.</summary>
    /// <param name="id">Price category identifier.</param>
    /// <response code="404">Price category not found.</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetPriceCategoryDto> GetPriceCategory(long id)
        => await priceCategoryService.GetPriceCategoryAsync(id);

    /// <summary>Updates an existing price category.</summary>
    /// <param name="id">Price category identifier.</param>
    /// <param name="dto">Updated price category data.</param>
    /// <response code="404">Price category not found.</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetPriceCategoryDto> UpdatePriceCategory(long id, [FromBody] SavePriceCategoryDto dto)
        => await priceCategoryService.UpdatePriceCategoryAsync(id, dto);

    /// <summary>Deletes a price category.</summary>
    /// <param name="id">Price category identifier.</param>
    /// <response code="204">Price category deleted.</response>
    /// <response code="404">Price category not found.</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePriceCategory(long id)
    {
        await priceCategoryService.DeletePriceCategoryAsync(id);
        return NoContent();
    }
}
