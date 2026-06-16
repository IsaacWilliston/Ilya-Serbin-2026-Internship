using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

public class SeatService(AppDbContext context) : ISeatService
{
    public async Task<GetSeatDto> CreatePlaceAsync(SaveSeatDto dto)
    {
        var entity = new SeatEntity
        {
            Row = dto.Row,
            Number = dto.Number,
            Status = dto.Status,
            IsAvailable = dto.IsAvailable,
            Comment = dto.Comment,
            HallId = dto.HallId!.Value,
            PriceCategoryId = dto.PriceCategoryId!.Value
        };
        context.Seats.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    public async Task<PagedResult<GetSeatDto>> GetAllPlacesAsync(int page, int size)
    {
        var query = context.Seats.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(s => s.Row).ThenBy(s => s.Number)
            .Skip(page * size)
            .Take(size)
            .Select(s => MapToDto(s))
            .ToListAsync();
        return new PagedResult<GetSeatDto>(items, total, page, size);
    }

    public async Task<GetSeatDto> GetPlaceAsync(long id)
    {
        var entity = await context.Seats.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");
        return MapToDto(entity);
    }

    public async Task<GetSeatDto> UpdatePlaceAsync(long id, SaveSeatDto dto)
    {
        var entity = await context.Seats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");
        entity.Row = dto.Row;
        entity.Number = dto.Number;
        entity.Status = dto.Status;
        entity.IsAvailable = dto.IsAvailable;
        entity.Comment = dto.Comment;
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    public async Task DeletePlaceAsync(long id)
    {
        var entity = await context.Seats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");
        context.Seats.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetSeatDto MapToDto(SeatEntity e) => new()
    {
        Id = e.Id,
        Row = e.Row,
        Number = e.Number,
        Status = e.Status,
        IsAvailable = e.IsAvailable,
        Comment = e.Comment
    };
}
