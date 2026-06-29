# Seats Reservation API (.NET)

A REST API for managing cinema seats and movie screening sessions. This project is a **.NET 9 port** of the original Java Spring Boot application (`SeatsReservation`), built with ASP.NET Core, Entity Framework Core, and PostgreSQL.

## What it does

The API lets you manage two core resources:

- **Places (`/places`)** — individual seats in a cinema hall (row, number, availability, price category).
- **Sessions (`/sessions`)** — scheduled movie screenings (movie, hall, date, time, language, format).

Behind the scenes, the database also stores cinemas, halls, movies, genres, price categories, and per-session seat bookings (`session_seats`), which link a session to a specific seat and optionally record a customer reservation.

## Domain model

```
Cinema → Hall → Seat (with PriceCategory)
Movie (with Genres)
Session (Movie + Hall) → SessionSeat (Session + Seat, holds customer booking)
```

All tables live in the PostgreSQL schema `base_schema`.

## Tech stack

| Layer | Technology |
|-------|------------|
| Runtime | .NET 9, C# 13 |
| Web | ASP.NET Core Web API |
| ORM | Entity Framework Core 9 + Npgsql |
| Database | PostgreSQL |
| API docs | Swagger (Swashbuckle) |

## Project structure

```
Controllers/     HTTP endpoints — validate input, return responses
Services/        Business logic — async CRUD via AppDbContext
DTOs/            Save*Dto (input), Get*Dto (output), PagedResult<T>
Entities/        EF Core entity classes mapped to PostgreSQL tables
Data/            AppDbContext and model configuration
Enums/           Stored as strings in the database (e.g. ACTIVE, PG_13, IMAX)
Migrations/      EF Core database migrations
```

Requests flow: **Controller → Service → DbContext → PostgreSQL**. Services throw `KeyNotFoundException` for missing records; the global exception handler in `Program.cs` maps these to HTTP 404 responses.

## API endpoints

Both resources support standard CRUD with pagination on list endpoints (`page` and `size` query params, default size 20).

| Method | Path | Description |
|--------|------|-------------|
| `POST` | `/places` | Create a seat |
| `GET` | `/places` | List seats (paginated) |
| `GET` | `/places/{id}` | Get a seat by ID |
| `PUT` | `/places/{id}` | Update a seat |
| `DELETE` | `/places/{id}` | Delete a seat |
| `POST` | `/sessions` | Create a session |
| `GET` | `/sessions` | List sessions (paginated) |
| `GET` | `/sessions/{id}` | Get a session by ID |
| `PUT` | `/sessions/{id}` | Update a session |
| `DELETE` | `/sessions/{id}` | Delete a session |

JSON responses serialize enums as strings and omit null fields.

Interactive API documentation is available at `/swagger` when the app is running.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (tested with PostgreSQL 18)
- `dotnet-ef` tool (for migrations):

  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Database setup

Connection settings are in `appsettings.json`:

| Setting | Default |
|---------|---------|
| Host | `localhost` |
| Port | `5432` |
| Database | `seats_reservation` |

Credentials are read from environment variables (not stored in config):

| Variable | Purpose |
|----------|---------|
| `DB_USERNAME` | PostgreSQL username |
| `DB_PASSWORD` | PostgreSQL password |

### 1. Create the database

```bash
psql -U postgres -c "CREATE DATABASE seats_reservation;"
```

### 2. Apply migrations

Because the connection uses `SearchPath=base_schema`, create the schema first if the database is empty:

```bash
psql -U postgres -d seats_reservation -c "CREATE SCHEMA IF NOT EXISTS base_schema;"
```

Then apply EF Core migrations:

```bash
# Linux / macOS
DB_USERNAME=postgres DB_PASSWORD=your_password dotnet ef database update

# Windows PowerShell
$env:DB_USERNAME="postgres"; $env:DB_PASSWORD="your_password"; dotnet ef database update
```

### 3. Seed sample data

```bash
psql -U postgres -d seats_reservation -f seed.sql
```

The seed file populates cinemas, halls, movies, genres, price categories, seats, sessions, and sample bookings.

## Run the application

```bash
# Linux / macOS
DB_USERNAME=postgres DB_PASSWORD=your_password dotnet run

# Windows PowerShell
$env:DB_USERNAME="postgres"; $env:DB_PASSWORD="your_password"; dotnet run
```

The API listens on `http://localhost:5267` (HTTP) and `https://localhost:7192` (HTTPS) by default. Open Swagger at `http://localhost:5267/swagger`.

## Build and test

```bash
dotnet build
dotnet test
```

## Example requests

Create a seat:

```http
POST /places
Content-Type: application/json

{
  "row": 1,
  "number": 5,
  "status": "ACTIVE",
  "isAvailable": true,
  "hallId": 1,
  "priceCategoryId": 2
}
```

Create a session:

```http
POST /sessions
Content-Type: application/json

{
  "movieId": 1,
  "hallId": 1,
  "title": "Interstellar - Evening",
  "date": "2026-06-28",
  "time": "19:00",
  "language": "ENGLISH",
  "format": "IMAX"
}
```

List sessions (page 0, 10 per page):

```http
GET /sessions?page=0&size=10
```
