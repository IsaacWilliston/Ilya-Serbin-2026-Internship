using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

/// <inheritdoc cref="IHallService"/>
public class HallService(AppDbContext context) : IHallService
{
    /// <inheritdoc/>
    public async Task<GetHallDto> CreateHallAsync(SaveHallDto dto)
    {
        var cinemaId = dto.CinemaId!.Value;
        if (!await context.Cinemas.AnyAsync(c => c.Id == cinemaId))
            throw new KeyNotFoundException($"Cinema with id {cinemaId} not found");

        var entity = new HallEntity
        {
            CinemaId = cinemaId,
            Name = dto.Name
        };
        context.Halls.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<GetHallDto>> GetAllHallsAsync(int page, int size)
    {
        var query = context.Halls.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(h => h.Name)
            .Skip(page * size)
            .Take(size)
            .Select(h => MapToDto(h))
            .ToListAsync();
        return new PagedResult<GetHallDto>(items, total, page, size);
    }

    /// <inheritdoc/>
    public async Task<GetHallDto> GetHallAsync(long id)
    {
        var entity = await context.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id)
            ?? throw new KeyNotFoundException($"Hall with id {id} not found");
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<GetHallDto> UpdateHallAsync(long id, SaveHallDto dto)
    {
        var entity = await context.Halls.FindAsync(id)
            ?? throw new KeyNotFoundException($"Hall with id {id} not found");

        var cinemaId = dto.CinemaId!.Value;
        if (!await context.Cinemas.AnyAsync(c => c.Id == cinemaId))
            throw new KeyNotFoundException($"Cinema with id {cinemaId} not found");

        entity.CinemaId = cinemaId;
        entity.Name = dto.Name;
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteHallAsync(long id)
    {
        var entity = await context.Halls.FindAsync(id)
            ?? throw new KeyNotFoundException($"Hall with id {id} not found");
        context.Halls.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetHallDto MapToDto(HallEntity e) => new()
    {
        Id = e.Id,
        CinemaId = e.CinemaId,
        Name = e.Name
    };
}
