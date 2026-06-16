using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CinemaEntity> Cinemas { get; set; }
    public DbSet<HallEntity> Halls { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<PriceCategoryEntity> PriceCategories { get; set; }
    public DbSet<SeatEntity> Seats { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<SessionSeatEntity> SessionSeats { get; set; }

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
    }
}
