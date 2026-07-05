using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

/// <inheritdoc cref="ICinemaService"/>
public class CinemaService(AppDbContext context) : ICinemaService
{
    /// <inheritdoc/>
    public async Task<GetCinemaDto> CreateCinemaAsync(SaveCinemaDto dto)
    {
        var entity = new CinemaEntity
        {
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City
        };
        context.Cinemas.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<GetCinemaDto>> GetAllCinemasAsync(int page, int size)
    {
        var query = context.Cinemas.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(c => c.Name)
            .Skip(page * size)
            .Take(size)
            .Select(c => MapToDto(c))
            .ToListAsync();
        return new PagedResult<GetCinemaDto>(items, total, page, size);
    }

    /// <inheritdoc/>
    public async Task<GetCinemaDto> GetCinemaAsync(long id)
    {
        var entity = await context.Cinemas.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new KeyNotFoundException($"Cinema with id {id} not found");
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<GetCinemaDto> UpdateCinemaAsync(long id, SaveCinemaDto dto)
    {
        var entity = await context.Cinemas.FindAsync(id)
            ?? throw new KeyNotFoundException($"Cinema with id {id} not found");
        entity.Name = dto.Name;
        entity.Address = dto.Address;
        entity.City = dto.City;
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteCinemaAsync(long id)
    {
        var entity = await context.Cinemas.FindAsync(id)
            ?? throw new KeyNotFoundException($"Cinema with id {id} not found");
        context.Cinemas.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetCinemaDto MapToDto(CinemaEntity e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        Address = e.Address,
        City = e.City
    };
}
