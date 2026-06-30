using Matrimony.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Matrimony.Domain.Entities.Masters;

namespace Matrimony.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }

        public DbSet<PartnerPreference> PartnerPreferences { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<ReligionMaster> ReligionMasters { get; set; }
        public DbSet<CasteMaster> CasteMasters { get; set; }
        public DbSet<MotherTongueMaster> MotherTongueMasters { get; set; }
        public DbSet<EducationMaster> EducationMasters { get; set; }
        public DbSet<OccupationMaster> OccupationMasters { get; set; }
        public DbSet<CountryMaster> CountryMasters { get; set; }
        public DbSet<StateMaster> StateMasters { get; set; }
        public DbSet<CityMaster> CityMasters { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser ↔ UserProfile (One-to-One)
            builder.Entity<UserProfile>()
                .HasOne(x => x.User)
                .WithOne(x => x.Profile)
                .HasForeignKey<UserProfile>(x => x.UserId);

            // Religion
            builder.Entity<UserProfile>()
                .HasOne(x => x.Religion)
                .WithMany()
                .HasForeignKey(x => x.ReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Caste
            builder.Entity<UserProfile>()
                .HasOne(x => x.Caste)
                .WithMany()
                .HasForeignKey(x => x.CasteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mother Tongue
            builder.Entity<UserProfile>()
                .HasOne(x => x.MotherTongue)
                .WithMany()
                .HasForeignKey(x => x.MotherTongueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Education
            builder.Entity<UserProfile>()
                .HasOne(x => x.Education)
                .WithMany()
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Occupation
            builder.Entity<UserProfile>()
                .HasOne(x => x.Occupation)
                .WithMany()
                .HasForeignKey(x => x.OccupationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Country
            builder.Entity<UserProfile>()
                .HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // State
            builder.Entity<UserProfile>()
                .HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // City
            builder.Entity<UserProfile>()
                .HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProfilePhoto
            builder.Entity<ProfilePhoto>()
                .HasOne(x => x.UserProfile)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
