using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserTaskApi.Models.Domain; // Include the namespace where your User model is defined

namespace UserTaskApi.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string adminUserId = Guid.NewGuid().ToString();
            string regularUserId = Guid.NewGuid().ToString();

            // Seed roles
            string adminRoleId = "e14f49d5-6c3f-4393-8610-a3c8cbdb618b";
            string userRoleId = "8a6a959b-e451-4d82-ad31-7e2377d1041b";

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new() {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name ="Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new() {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name ="User",
                    NormalizedName = "User".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed admin user
            var hasher = new PasswordHasher<User>();

            builder.Entity<User>().HasData(new User
            {
                Id = adminUserId,
                UserName = "admin@example.com",
                NormalizedUserName = "admin@example.com".ToUpper(),
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Admin",
                LastName = "User"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

            // Seed regular user
            builder.Entity<User>().HasData(new User
            {
                Id = regularUserId,
                UserName = "user@example.com",
                NormalizedUserName = "user@example.com".ToUpper(),
                Email = "user@example.com",
                NormalizedEmail = "user@example.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "User@123"),
                SecurityStamp = string.Empty,
                FirstName = "John",
                LastName = "Doe"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = regularUserId
            });
        }
    }
}
