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
        protected override void OnModelCreating(
        ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}
