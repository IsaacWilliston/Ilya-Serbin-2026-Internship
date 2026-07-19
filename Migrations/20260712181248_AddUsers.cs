using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "base_schema",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "base_schema",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "base_schema",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "base_schema",
                newName: "users",
                newSchema: "base_schema");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "base_schema",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "base_schema",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "base_schema",
                table: "users",
                newName: "password_hash");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                schema: "base_schema",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "base_schema",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                schema: "base_schema",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "base_schema",
                table: "users",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "base_schema",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "base_schema",
                newName: "Users",
                newSchema: "base_schema");

            migrationBuilder.RenameColumn(
                name: "role",
                schema: "base_schema",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "base_schema",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "base_schema",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                schema: "base_schema",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "base_schema",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "base_schema",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                schema: "base_schema",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "base_schema",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "base_schema",
                table: "Users",
                column: "id");
        }
    }
}
