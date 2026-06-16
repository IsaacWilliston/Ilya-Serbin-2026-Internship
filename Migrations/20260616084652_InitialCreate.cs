using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base_schema");

            migrationBuilder.CreateTable(
                name: "cinemas",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    city = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinemas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    durationminutes = table.Column<int>(name: "duration-minutes", type: "integer", nullable: true),
                    agerating = table.Column<string>(name: "age-rating", type: "character varying(50)", maxLength: 50, nullable: true),
                    rating = table.Column<float>(type: "real", nullable: true),
                    posterurl = table.Column<string>(name: "poster-url", type: "character varying(1000)", maxLength: 1000, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    releaseyear = table.Column<int>(name: "release-year", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "price_category",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    price = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "halls",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cinema_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_halls", x => x.id);
                    table.ForeignKey(
                        name: "FK_halls_cinemas_cinema_id",
                        column: x => x.cinema_id,
                        principalSchema: "base_schema",
                        principalTable: "cinemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                schema: "base_schema",
                columns: table => new
                {
                    movie_id = table.Column<long>(type: "bigint", nullable: false),
                    genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_genres", x => new { x.movie_id, x.genre });
                    table.ForeignKey(
                        name: "FK_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "base_schema",
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seats",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hall_id = table.Column<long>(type: "bigint", nullable: false),
                    price_category_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: true),
                    number = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_available = table.Column<bool>(type: "boolean", nullable: true),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.id);
                    table.ForeignKey(
                        name: "FK_seats_halls_hall_id",
                        column: x => x.hall_id,
                        principalSchema: "base_schema",
                        principalTable: "halls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seats_price_category_price_category_id",
                        column: x => x.price_category_id,
                        principalSchema: "base_schema",
                        principalTable: "price_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movie_id = table.Column<long>(type: "bigint", nullable: false),
                    hall_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    language = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    format = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_sessions_halls_hall_id",
                        column: x => x.hall_id,
                        principalSchema: "base_schema",
                        principalTable: "halls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sessions_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "base_schema",
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_seats",
                schema: "base_schema",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    session_id = table.Column<long>(type: "bigint", nullable: false),
                    seat_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_available = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    customer_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    contact = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_seats", x => x.id);
                    table.ForeignKey(
                        name: "FK_session_seats_seats_seat_id",
                        column: x => x.seat_id,
                        principalSchema: "base_schema",
                        principalTable: "seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_session_seats_sessions_session_id",
                        column: x => x.session_id,
                        principalSchema: "base_schema",
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_halls_cinema_id",
                schema: "base_schema",
                table: "halls",
                column: "cinema_id");

            migrationBuilder.CreateIndex(
                name: "idx_movie_genres_movie_id",
                schema: "base_schema",
                table: "movie_genres",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "idx_seats_hall_id",
                schema: "base_schema",
                table: "seats",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "idx_seats_price_category_id",
                schema: "base_schema",
                table: "seats",
                column: "price_category_id");

            migrationBuilder.CreateIndex(
                name: "idx_session_seats_seat_id",
                schema: "base_schema",
                table: "session_seats",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "idx_session_seats_session_id",
                schema: "base_schema",
                table: "session_seats",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "idx_sessions_hall_id",
                schema: "base_schema",
                table: "sessions",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "idx_sessions_movie_id",
                schema: "base_schema",
                table: "sessions",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_genres",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "session_seats",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "seats",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "sessions",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "price_category",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "halls",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "movies",
                schema: "base_schema");

            migrationBuilder.DropTable(
                name: "cinemas",
                schema: "base_schema");
        }
    }
}
