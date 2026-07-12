using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Data;

/// <summary>
/// Entity Framework Core database context for the cinema reservation schema.
/// All tables are mapped to the <c>base_schema</c> PostgreSQL schema.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Cinema venues.</summary>
    public DbSet<CinemaEntity> Cinemas { get; set; }

    /// <summary>Screening halls within cinemas.</summary>
    public DbSet<HallEntity> Halls { get; set; }

    /// <summary>Movies available for screening.</summary>
    public DbSet<MovieEntity> Movies { get; set; }

    /// <summary>Movie-to-genre join records.</summary>
    public DbSet<MovieGenre> MovieGenres { get; set; }

    /// <summary>Seat pricing tiers (VIP, standard, economy).</summary>
    public DbSet<PriceCategoryEntity> PriceCategories { get; set; }

    /// <summary>Physical seats within halls.</summary>
    public DbSet<SeatEntity> Seats { get; set; }

    /// <summary>Scheduled movie screenings.</summary>
    public DbSet<SessionEntity> Sessions { get; set; }

    /// <summary>Per-session seat bookings.</summary>
    public DbSet<SessionSeatEntity> SessionSeats { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("base_schema");

        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.Genre });

        modelBuilder.Entity<MovieGenre>()
            .Property(mg => mg.Genre)
            .HasConversion<string>();

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HallEntity>()
            .HasOne(h => h.Cinema)
            .WithMany(c => c.Halls)
            .HasForeignKey(h => h.CinemaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SeatEntity>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Seats)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SessionEntity>()
            .HasOne(s => s.Movie)
            .WithMany()
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SessionEntity>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Sessions)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SessionSeatEntity>()
            .HasOne(ss => ss.Session)
            .WithMany()
            .HasForeignKey(ss => ss.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SessionSeatEntity>()
            .HasOne(ss => ss.Seat)
            .WithMany()
            .HasForeignKey(ss => ss.SeatId)
            .OnDelete(DeleteBehavior.Cascade);

        // Store all enums as strings to match the PostgreSQL VARCHAR columns
        modelBuilder.Entity<MovieEntity>()
            .Property(m => m.AgeRating).HasConversion<string>();

        modelBuilder.Entity<PriceCategoryEntity>()
            .Property(pc => pc.Type).HasConversion<string>();

        modelBuilder.Entity<SeatEntity>()
            .Property(s => s.Status).HasConversion<string>();

        modelBuilder.Entity<SessionEntity>()
            .Property(s => s.Language).HasConversion<string>();

        modelBuilder.Entity<SessionEntity>()
            .Property(s => s.Format).HasConversion<string>();

        modelBuilder.Entity<SessionSeatEntity>()
            .Property(ss => ss.Status).HasConversion<string>();
        
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Role).HasConversion<string>();

        // Indexes matching the SQL schema
        modelBuilder.Entity<HallEntity>()
            .HasIndex(h => h.CinemaId).HasDatabaseName("idx_halls_cinema_id");
        modelBuilder.Entity<MovieGenre>()
            .HasIndex(mg => mg.MovieId).HasDatabaseName("idx_movie_genres_movie_id");
        modelBuilder.Entity<SeatEntity>()
            .HasIndex(s => s.HallId).HasDatabaseName("idx_seats_hall_id");
        modelBuilder.Entity<SeatEntity>()
            .HasIndex(s => s.PriceCategoryId).HasDatabaseName("idx_seats_price_category_id");
        modelBuilder.Entity<SessionEntity>()
            .HasIndex(s => s.MovieId).HasDatabaseName("idx_sessions_movie_id");
        modelBuilder.Entity<SessionEntity>()
            .HasIndex(s => s.HallId).HasDatabaseName("idx_sessions_hall_id");
        modelBuilder.Entity<SessionSeatEntity>()
            .HasIndex(ss => ss.SessionId).HasDatabaseName("idx_session_seats_session_id");
        modelBuilder.Entity<SessionSeatEntity>()
            .HasIndex(ss => ss.SeatId).HasDatabaseName("idx_session_seats_seat_id");
        
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .HasDatabaseName("idx_users_email")
            .IsUnique(); 
    }
}
