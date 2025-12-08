using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal_AP1_AdonisMercado.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // IDs para roles y usuarios
        var adminRoleId = "1";
        var userRoleId = "2";
        var adminUserId = "1";
        var normalUserId = "2";

        // Crear roles con ConcurrencyStamp estático
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "4bc8927a-fa27-4f6d-a171-70a67af741bb"
            },
            new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "f8e7d37d-cc30-47b3-81a1-c31e398bcd4b"
            }
        );

        // Crear usuarios con todos los valores estáticos
        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                // Hash real para "Admin123!"
                PasswordHash = "AQAAAAIAAYagAAAAEN6rAk/gzrQJc9hDUhFAL2EQqqp8tcsR2NLZo/Ec1GwFg/IGPgeWPexOU9VZZkDX0w==",
                SecurityStamp = "ADMIN-SECURITY-STAMP-12345",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            },
            new ApplicationUser
            {
                Id = normalUserId,
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.COM",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                // Hash real para "User123!"
                PasswordHash = "AQAAAAIAAYagAAAAEBIND24eOBbqSfrIqa3V0zur/Or+EjBMd+55omDsgEH+H1J7HSOx3AvlmeLKbIQsdg==",
                SecurityStamp = "USER-SECURITY-STAMP-67890",
                ConcurrencyStamp = "d9665377-c512-5620-0bfb-b0394164gd69",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );

        // Asignar roles
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = normalUserId
            }
        );
    }
}