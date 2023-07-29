using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Translations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.EF.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<GenderTranslation> GenderTranslations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<RefreshToken>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Gender>().HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<RefreshToken>()
                .HasIndex(x => x.UserName);

            builder.Entity<User>()
                .HasOne(x => x.Gender)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasOne(x => x.SearchingGender)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GenderTranslation>()
                .HasIndex(x => new { x.GenderId, x.CultureName })
                .IsUnique();

            Seed(builder);

            base.OnModelCreating(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            SeedGenders(builder);
            SeedIdentity(builder);
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
                    PasswordHash = passwordHasher.HashPassword(null, "admin"),
                    GenderId = 1,
                    SearchingGenderId = 1
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

        private void SeedGenders(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(new Gender()
            {
                Id = 1,
                Name = "Male"
            });

            builder.Entity<Gender>().HasData(new Gender()
            {
                Id = 2,
                Name = "Female"
            });
        }
    }
}
