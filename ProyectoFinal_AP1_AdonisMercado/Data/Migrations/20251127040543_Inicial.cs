using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoFinal_AP1_AdonisMercado.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b3f1c8b6-9f8d-4b5e-9f1a-0a1b2c3d4e01", "e1f2a3b4-5c6d-7e8f-9001-234567890001" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d4e2a6c9-7b3f-4a1b-8c9d-1e2f3a4b5c02", "f2e3d4c5-b6a7-8c9d-0012-345678900002" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3f1c8b6-9f8d-4b5e-9f1a-0a1b2c3d4e01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4e2a6c9-7b3f-4a1b-8c9d-1e2f3a4b5c02");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1f2a3b4-5c6d-7e8f-9001-234567890001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2e3d4c5-b6a7-8c9d-0012-345678900002");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b3f1c8b6-9f8d-4b5e-9f1a-0a1b2c3d4e01", "0c9a1f6a-7d2b-4d4b-8a2b-111111111111", "Admin", "ADMIN" },
                    { "d4e2a6c9-7b3f-4a1b-8c9d-1e2f3a4b5c02", "1d2b3c4d-5e6f-4789-9a2b-222222222222", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e1f2a3b4-5c6d-7e8f-9001-234567890001", 0, "4b5c6d7e-8f90-4123-4567-444444444444", "admin@local.test", true, false, null, "ADMIN@LOCAL.TEST", "ADMIN@LOCAL.TEST", "AQAAAAEAACcQAAAAEO0Z6bqF1F2wz7nY7bKp1sI7Qw3h3KXk2w9V3u5zQxqV6J4eT1a2b3c4d5e6f7g==", null, false, "3a4b5c6d-7e8f-4901-2345-333333333333", false, "admin@local.test" },
                    { "f2e3d4c5-b6a7-8c9d-0012-345678900002", 0, "6d7e8f90-1234-4345-6789-666666666666", "user@local.test", true, false, null, "USER@LOCAL.TEST", "USER@LOCAL.TEST", "AQAAAAEAACcQAAAAEK9Yt7cP4sT6nX8uWq9r8s7t6u5v4w3x2y1z0a9b8c7d6e5f4g3h2i1j0k==", null, false, "5c6d7e8f-9012-4234-5678-555555555555", false, "user@local.test" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b3f1c8b6-9f8d-4b5e-9f1a-0a1b2c3d4e01", "e1f2a3b4-5c6d-7e8f-9001-234567890001" },
                    { "d4e2a6c9-7b3f-4a1b-8c9d-1e2f3a4b5c02", "f2e3d4c5-b6a7-8c9d-0012-345678900002" }
                });
        }
    }
}
