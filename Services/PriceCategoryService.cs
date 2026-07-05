using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

/// <inheritdoc cref="IPriceCategoryService"/>
public class PriceCategoryService(AppDbContext context) : IPriceCategoryService
{
    /// <inheritdoc/>
    public async Task<GetPriceCategoryDto> CreatePriceCategoryAsync(SavePriceCategoryDto dto)
    {
        var entity = new PriceCategoryEntity
        {
            Type = dto.Type,
            Name = dto.Name,
            Price = dto.Price
        };
        context.PriceCategories.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategoriesAsync(int page, int size)
    {
        var query = context.PriceCategories.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(pc => pc.Name)
            .Skip(page * size)
            .Take(size)
            .Select(pc => MapToDto(pc))
            .ToListAsync();
        return new PagedResult<GetPriceCategoryDto>(items, total, page, size);
    }

    /// <inheritdoc/>
    public async Task<GetPriceCategoryDto> GetPriceCategoryAsync(long id)
    {
        var entity = await context.PriceCategories.AsNoTracking().FirstOrDefaultAsync(pc => pc.Id == id)
            ?? throw new KeyNotFoundException($"Price category with id {id} not found");
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<GetPriceCategoryDto> UpdatePriceCategoryAsync(long id, SavePriceCategoryDto dto)
    {
        var entity = await context.PriceCategories.FindAsync(id)
            ?? throw new KeyNotFoundException($"Price category with id {id} not found");
        entity.Type = dto.Type;
        entity.Name = dto.Name;
        entity.Price = dto.Price;
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task DeletePriceCategoryAsync(long id)
    {
        var entity = await context.PriceCategories.FindAsync(id)
            ?? throw new KeyNotFoundException($"Price category with id {id} not found");
        context.PriceCategories.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetPriceCategoryDto MapToDto(PriceCategoryEntity e) => new()
    {
        Id = e.Id,
        Type = e.Type,
        Name = e.Name,
        Price = e.Price
    };
}
