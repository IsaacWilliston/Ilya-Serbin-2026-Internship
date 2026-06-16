# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run (requires DB_USERNAME and DB_PASSWORD env vars)
DB_USERNAME=postgres DB_PASSWORD=secret dotnet run

# Run tests
dotnet test

# EF Core migrations (run after changing entities)
dotnet ef migrations add <MigrationName>
dotnet ef database update

# Scaffold from existing DB instead of code-first
dotnet ef dbcontext scaffold "Host=...;..." Npgsql.EntityFrameworkCore.PostgreSQL \
  --schema base_schema --output-dir Entities --force
```

## Environment variables

| Variable | Purpose |
|---|---|
| `DB_USERNAME` | PostgreSQL username |
| `DB_PASSWORD` | PostgreSQL password |

Database host/port/name are configured in `appsettings.json` under `Database:*`.

## Architecture

ASP.NET Core 9 Web API, C# 13, .NET 9. Entity Framework Core 9 with Npgsql (PostgreSQL). Swagger via Swashbuckle.

**This is a .NET port of the Java Spring Boot project at `../SeatsReservation`.**

**Domain model:**
```
Cinema → Hall → Seat (with PriceCategory)
Movie (with Genres via movie_genres join table)
Session (Movie + Hall) → SessionSeat (Session + Seat, holds customer booking)
```

All tables live in the `base_schema` PostgreSQL schema, set via `HasDefaultSchema("base_schema")` in `AppDbContext`.

**Layer conventions:**
- Controllers (`/places`, `/sessions`) — validate input, delegate to services, return HTTP responses.
- Services (`SeatService`, `SessionService`) — all async, use `AppDbContext` directly (no extra repository layer). Throw `KeyNotFoundException` for 404 cases; `Program.cs` maps this to a 404 response.
- DTOs — `Save*Dto` for input (with `[Required]` validation), `Get*Dto` for output. `PagedResult<T>` mirrors Spring's `Page<T>` response shape.
- Enums — all stored as strings in the DB via `.HasConversion<string>()` in `AppDbContext.OnModelCreating`.

**Enum storage:** All enums use `HasConversion<string>()` configured in `OnModelCreating`, so the string values written to PostgreSQL match the Java app's `@Enumerated(EnumType.STRING)` output exactly.

**Movie genres** are a separate `movie_genres` table with a composite PK `(movie_id, genre)` — modeled as `MovieGenre` entity with `HasKey(mg => new { mg.MovieId, mg.Genre })`.

**Columns with hyphens** (`duration-minutes`, `age-rating`, `poster-url`, `release-year` on `MovieEntity`) are mapped via `[Column("duration-minutes")]` etc. — these require quoted identifiers in raw SQL.

**Improvements over the Java original:**
- Services are fully implemented (Java had stubs returning empty builders).
- `SaveSeatDto` includes `HallId` and `PriceCategoryId` (Java DTOs omitted these required FK fields).
- `SaveSessionDto` includes `HallId`.
- Global exception handler in `Program.cs` converts `KeyNotFoundException` → 404 with a JSON body.
- JSON serializes enums as strings and omits null fields.
