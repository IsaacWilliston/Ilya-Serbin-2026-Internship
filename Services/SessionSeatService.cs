using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Services;

/// <inheritdoc cref="ISessionSeatService"/>
public class SessionSeatService(AppDbContext context) : ISessionSeatService
{
    /// <inheritdoc/>
    public async Task<GetSessionSeatDto> CreateSessionSeatAsync(SaveSessionSeatDto dto)
    {
        var sessionId = dto.SessionId!.Value;
        var seatId = dto.SeatId!.Value;

        if (!await context.Sessions.AnyAsync(s => s.Id == sessionId))
            throw new KeyNotFoundException($"Session with id {sessionId} not found");
        if (!await context.Seats.AnyAsync(s => s.Id == seatId))
            throw new KeyNotFoundException($"Seat with id {seatId} not found");

        var entity = new SessionSeatEntity
        {
            SessionId = sessionId,
            SeatId = seatId,
            Status = dto.Status ?? SeatStatus.ACTIVE,
            IsAvailable = dto.IsAvailable ?? "false",
            CustomerName = dto.CustomerName,
            Contact = dto.Contact
        };
        context.SessionSeats.Add(entity);
        await context.SaveChangesAsync();

        await context.Entry(entity).Reference(ss => ss.Session).LoadAsync();
        await context.Entry(entity).Reference(ss => ss.Seat).LoadAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeatsAsync(int page, int size)
    {
        var query = context.SessionSeats.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .Include(ss => ss.Session)
            .Include(ss => ss.Seat)
            .OrderBy(ss => ss.SessionId).ThenBy(ss => ss.SeatId)
            .Skip(page * size)
            .Take(size)
            .ToListAsync();
        return new PagedResult<GetSessionSeatDto>(items.Select(MapToDto).ToList(), total, page, size);
    }

    /// <inheritdoc/>
    public async Task<GetSessionSeatDto> GetSessionSeatAsync(long id)
    {
        var entity = await context.SessionSeats.AsNoTracking()
            .Include(ss => ss.Session)
            .Include(ss => ss.Seat)
            .FirstOrDefaultAsync(ss => ss.Id == id)
            ?? throw new KeyNotFoundException($"Session seat with id {id} not found");
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<GetSessionSeatDto> UpdateSessionSeatAsync(long id, SaveSessionSeatDto dto)
    {
        var entity = await context.SessionSeats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session seat with id {id} not found");

        var sessionId = dto.SessionId!.Value;
        var seatId = dto.SeatId!.Value;

        if (!await context.Sessions.AnyAsync(s => s.Id == sessionId))
            throw new KeyNotFoundException($"Session with id {sessionId} not found");
        if (!await context.Seats.AnyAsync(s => s.Id == seatId))
            throw new KeyNotFoundException($"Seat with id {seatId} not found");

        entity.SessionId = sessionId;
        entity.SeatId = seatId;
        entity.Status = dto.Status ?? entity.Status;
        entity.IsAvailable = dto.IsAvailable ?? entity.IsAvailable;
        entity.CustomerName = dto.CustomerName;
        entity.Contact = dto.Contact;
        await context.SaveChangesAsync();

        await context.Entry(entity).Reference(ss => ss.Session).LoadAsync();
        await context.Entry(entity).Reference(ss => ss.Seat).LoadAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteSessionSeatAsync(long id)
    {
        var entity = await context.SessionSeats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session seat with id {id} not found");
        context.SessionSeats.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetSessionSeatDto MapToDto(SessionSeatEntity e) => new()
    {
        Id = e.Id,
        Session = e.Session != null ? MapSessionToDto(e.Session) : null,
        Place = e.Seat != null ? MapSeatToDto(e.Seat) : null,
        Status = e.Status,
        IsAvailable = e.IsAvailable,
        CustomerName = e.CustomerName,
        Contact = e.Contact
    };

    private static GetSessionDto MapSessionToDto(SessionEntity e) => new()
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

    private static GetSeatDto MapSeatToDto(SeatEntity e) => new()
    {
        Id = e.Id,
        Row = e.Row,
        Number = e.Number,
        Status = e.Status,
        IsAvailable = e.IsAvailable,
        Comment = e.Comment
    };
}
