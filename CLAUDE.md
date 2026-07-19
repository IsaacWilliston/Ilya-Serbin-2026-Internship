# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run (requires DB_USERNAME, DB_PASSWORD and JWT_SIGNING_KEY env vars)
DB_USERNAME=postgres DB_PASSWORD=secret JWT_SIGNING_KEY=your_very_secret_key_32_chars_long dotnet run

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
| `JWT_SIGNING_KEY` | Secret key for JWT signing (min 32 chars) |

Database host/port/name are configured in `appsettings.json` under `Database:*`.

## Architecture

ASP.NET Core 9 Web API, C# 13, .NET 9. Entity Framework Core 9 with Npgsql (PostgreSQL). Swagger via Swashbuckle.

**This is a .NET port of the Java Spring Boot project at `../SeatsReservation`.**

**Domain model:**
```
Cinema → Hall → Seat (with PriceCategory)
Movie (with Genres via movie_genres join table)
Session (Movie + Hall) → SessionSeat (Session + Seat, holds customer booking)
User (Email, PasswordHash, Role)
```

All tables live in the `base_schema` PostgreSQL schema, set via `HasDefaultSchema("base_schema")` in `AppDbContext`.

**Auth & Roles:**
- **Roles:** `ADMIN`, `CUSTOMER`.
- **Admin:** Full CRUD on all resources.
- **Customer:** Can view cinemas/movies/sessions and create bookings (`POST /session-seats`).
- **Registration:** New users are registered as `CUSTOMER` by default. Admins must be promoted manually in the database (setting `role = 'ADMIN'`).
- **Testing:** Get token via `POST /auth/login`. In Swagger, click "Authorize" and enter `Bearer <token>`.

**Layer conventions:**
- Controllers (`/cinemas`, `/halls`, `/movies`, `/places`, `/price-categories`, `/sessions`, `/session-seats`, `/auth`) — validate input, delegate to services, return HTTP responses.
- Services (`SeatService`, `AuthService`, `SessionService`) — all async, use `AppDbContext` directly (no extra repository layer). Throw `KeyNotFoundException` (404), `UnauthorizedAccessException` (401), or `InvalidOperationException` (409); `Program.cs` maps these to appropriate JSON responses.
- DTOs — `Save*Dto` for input (with `[Required]` validation), `Get*Dto` for output.
- Enums — all stored as strings in the DB via `.HasConversion<string>()` in `AppDbContext.OnModelCreating`.

**Key DTO Shapes:**
- `RegisterDto`: `Email` (required), `Password` (required, min 8 chars).
- `SaveMovieDto`: `Title` (no `[Required]` in code — known gap), `DurationMinutes` (required), `Genres` (list of `Genre` enum values), `AgeRating`, `PosterUrl`, `ReleaseYear`, `Rating`.
- `SaveSessionSeatDto`: `SessionId`, `SeatId`, `CustomerName`, `Contact` (all required).

**Movie search endpoint:**
`GET /movies/search` — anonymous, accepts optional query params:
- `title` — case-insensitive partial match (Postgres `ILIKE`).
- `genre` — one of the `Genre` enum values (e.g. `ACTION`, `DRAMA`). Movie must have that genre in its `Genres` collection.
- `year` — exact match on `ReleaseYear`.
- `page` (default `0`), `size` (default `20`).
Returns `PagedResult<GetMovieDto>` with results in the `content` field.

**Developer tools in the repo:**
- `SeatsReservationDotNet.http` / `week-3-demo.http` / `week-4-demo.http` — Rider HTTP Client files for manual API testing. Set `baseUrl` in `http-client.env.json`.
- `movies.html` — standalone vanilla-JS frontend for browsing/searching movies. Open directly in a browser while the API is running. Edit the `BASE` constant at the top of the script to match your active launch profile URL.

**Enum storage:** All enums use `HasConversion<string>()` configured in `OnModelCreating`, so the string values written to PostgreSQL match the Java app's `@Enumerated(EnumType.STRING)` output exactly.

**Movie genres** are a separate `movie_genres` table with a composite PK `(movie_id, genre)` — modeled as `MovieGenre` entity with `HasKey(mg => new { mg.MovieId, mg.Genre })`.

**Columns with hyphens** (`duration-minutes`, `age-rating`, `poster-url`, `release-year` on `MovieEntity`) are mapped via `[Column("duration-minutes")]` etc. — these require quoted identifiers in raw SQL.

**Improvements over the Java original:**
- Services are fully implemented (Java had stubs returning empty builders).
- `SaveSeatDto` includes `HallId` and `PriceCategoryId` (Java DTOs omitted these required FK fields).
- `SaveSessionDto` includes `HallId`.
- Global exception handler in `Program.cs` converts domain exceptions to appropriate 404, 401, or 409 responses.
- JSON serializes enums as strings and omits null fields.
