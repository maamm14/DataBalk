using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserTaskApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "52ed6d4d-790c-4655-8e92-28cebb2d08d2", 0, "429e81e3-6772-4f61-997d-c20702b8e84f", "admin@example.com", true, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJXa8Lwg2U/GtKzJuLmxGfHgLUvkevlBfIsOUCh3NCupqOtquNaYTeZ4RpGnJLAsPA==", null, false, "", false, "admin@example.com" },
                    { "75b31e4d-4fb4-455c-8274-9658df7aa950", 0, "7d275650-826d-4174-b17a-e1a151b0bba4", "user@example.com", true, "John", "Doe", false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKFXcv1q33h4ldSF61uWU233Uloy8E99dDxyozytIxdPIXhT+fuMhgchHIS5OSmFjQ==", null, false, "", false, "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e14f49d5-6c3f-4393-8610-a3c8cbdb618b", "52ed6d4d-790c-4655-8e92-28cebb2d08d2" },
                    { "8a6a959b-e451-4d82-ad31-7e2377d1041b", "75b31e4d-4fb4-455c-8274-9658df7aa950" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e14f49d5-6c3f-4393-8610-a3c8cbdb618b", "52ed6d4d-790c-4655-8e92-28cebb2d08d2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8a6a959b-e451-4d82-ad31-7e2377d1041b", "75b31e4d-4fb4-455c-8274-9658df7aa950" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52ed6d4d-790c-4655-8e92-28cebb2d08d2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75b31e4d-4fb4-455c-8274-9658df7aa950");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
