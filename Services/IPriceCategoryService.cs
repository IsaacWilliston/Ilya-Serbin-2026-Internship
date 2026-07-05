using SeatsReservationDotNet.DTOs;

namespace SeatsReservationDotNet.Services;

/// <summary>Operations for managing seat price categories.</summary>
public interface IPriceCategoryService
{
    /// <summary>Creates a new price category.</summary>
    Task<GetPriceCategoryDto> CreatePriceCategoryAsync(SavePriceCategoryDto dto);

    /// <summary>Returns a paginated list of price categories ordered by name.</summary>
    Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategoriesAsync(int page, int size);

    /// <summary>Returns a price category by identifier.</summary>
    /// <exception cref="KeyNotFoundException">Price category does not exist.</exception>
    Task<GetPriceCategoryDto> GetPriceCategoryAsync(long id);

    /// <summary>Updates an existing price category.</summary>
    /// <exception cref="KeyNotFoundException">Price category does not exist.</exception>
    Task<GetPriceCategoryDto> UpdatePriceCategoryAsync(long id, SavePriceCategoryDto dto);

    /// <summary>Deletes a price category.</summary>
    /// <exception cref="KeyNotFoundException">Price category does not exist.</exception>
    Task DeletePriceCategoryAsync(long id);
}
