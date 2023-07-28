using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.EF.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<RefreshToken>().HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<RefreshToken>()
                .HasIndex(x => x.UserName);

            SeedIdentity(builder);

            base.OnModelCreating(builder);
        }

        private void SeedIdentity(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<User>();

            builder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    UserName = "admin",
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    NormalizedUserName = "ADMIN",
                    Email = "admin",
                    NormalizedEmail = "ADMIN",
                    About = string.Empty,
                    City = "adminLand",
                    PasswordHash = passwordHasher.HashPassword(null, "admin")
                });

            builder.Entity<Role>()
                .HasData(new Role()
                {
                    Id = 1,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                });

            builder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 1
                });
        }
    }
}
