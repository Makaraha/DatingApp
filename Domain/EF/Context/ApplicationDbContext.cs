using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Domain.Interfaces;

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

            base.OnModelCreating(builder);
        }
    }
}
