using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

public class SessionService(AppDbContext context) : ISessionService
{
    public async Task<GetSessionDto> CreateSessionAsync(SaveSessionDto dto)
    {
        var entity = new SessionEntity
        {
            MovieId = dto.MovieId!.Value,
            HallId = dto.HallId!.Value,
            Title = dto.Title,
            Date = dto.Date,
            Time = dto.Time,
            Language = dto.Language,
            Format = dto.Format
        };
        context.Sessions.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    public async Task<PagedResult<GetSessionDto>> GetAllSessionsAsync(int page, int size)
    {
        var query = context.Sessions.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(s => s.Date).ThenBy(s => s.Time).ThenBy(s => s.Title)
            .Skip(page * size)
            .Take(size)
            .Select(s => MapToDto(s))
            .ToListAsync();
        return new PagedResult<GetSessionDto>(items, total, page, size);
    }

    public async Task<GetSessionDto> GetSessionAsync(long id)
    {
        var entity = await context.Sessions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");
        return MapToDto(entity);
    }

    public async Task<GetSessionDto> UpdateSessionAsync(long id, SaveSessionDto dto)
    {
        var entity = await context.Sessions.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");
        entity.Title = dto.Title;
        entity.Date = dto.Date;
        entity.Time = dto.Time;
        entity.Language = dto.Language;
        entity.Format = dto.Format;
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    public async Task DeleteSessionAsync(long id)
    {
        var entity = await context.Sessions.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");
        context.Sessions.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetSessionDto MapToDto(SessionEntity e) => new()
    {
        Id = e.Id,
        MovieId = e.MovieId,
        HallId = e.HallId,
        Title = e.Title,
        Date = e.Date,
        Time = e.Time,
        Language = e.Language,
        Format = e.Format
    };
}
