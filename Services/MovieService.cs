using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Services;

/// <inheritdoc cref="IMovieService"/>
public class MovieService(AppDbContext context) : IMovieService
{
    /// <inheritdoc/>
    public async Task<GetMovieDto> CreateMovieAsync(SaveMovieDto dto)
    {
        var entity = new MovieEntity
        {
            Title = dto.Title,
            DurationMinutes = dto.DurationMinutes,
            AgeRating = dto.AgeRating,
            Rating = dto.Rating,
            PosterUrl = dto.PosterUrl,
            Description = dto.Description,
            ReleaseYear = dto.ReleaseYear
        };

        foreach (var genre in dto.Genres)
            entity.Genres.Add(new MovieGenre { Genre = genre });

        context.Movies.Add(entity);
        await context.SaveChangesAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<PagedResult<GetMovieDto>> GetAllMoviesAsync(int page, int size)
    {
        var query = context.Movies.AsNoTracking();
        var total = await query.CountAsync();
        var items = await query
            .Include(m => m.Genres)
            .OrderBy(m => m.Title)
            .Skip(page * size)
            .Take(size)
            .ToListAsync();
        return new PagedResult<GetMovieDto>(items.Select(MapToDto).ToList(), total, page, size);
    }

    /// <inheritdoc/>
    public async Task<GetMovieDto> GetMovieAsync(long id)
    {
        var entity = await context.Movies.AsNoTracking()
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new KeyNotFoundException($"Movie with id {id} not found");
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<GetMovieDto> UpdateMovieAsync(long id, SaveMovieDto dto)
    {
        var entity = await context.Movies.FindAsync(id)
            ?? throw new KeyNotFoundException($"Movie with id {id} not found");

        entity.Title = dto.Title;
        entity.DurationMinutes = dto.DurationMinutes;
        entity.AgeRating = dto.AgeRating;
        entity.Rating = dto.Rating;
        entity.PosterUrl = dto.PosterUrl;
        entity.Description = dto.Description;
        entity.ReleaseYear = dto.ReleaseYear;

        var existingGenres = await context.MovieGenres
            .Where(mg => mg.MovieId == id)
            .ToListAsync();
        context.MovieGenres.RemoveRange(existingGenres);

        var newGenres = dto.Genres
            .Select(genre => new MovieGenre { MovieId = id, Genre = genre })
            .ToList();
        context.MovieGenres.AddRange(newGenres);

        await context.SaveChangesAsync();
        await context.Entry(entity).Collection(e => e.Genres).LoadAsync();
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteMovieAsync(long id)
    {
        var entity = await context.Movies.FindAsync(id)
            ?? throw new KeyNotFoundException($"Movie with id {id} not found");
        context.Movies.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static GetMovieDto MapToDto(MovieEntity e) => new()
    {
        Id = e.Id,
        Title = e.Title,
        DurationMinutes = e.DurationMinutes,
        AgeRating = e.AgeRating,
        Rating = e.Rating,
        PosterUrl = e.PosterUrl,
        Description = e.Description,
        Genres = e.Genres.Select(g => g.Genre),
        ReleaseYear = e.ReleaseYear
    };
}
