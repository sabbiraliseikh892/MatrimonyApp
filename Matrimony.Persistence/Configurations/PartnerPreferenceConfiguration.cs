using Matrimony.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Persistence.Configurations
{
    public class PartnerPreferenceConfiguration
        : IEntityTypeConfiguration<PartnerPreference>
    {
        public void Configure(EntityTypeBuilder<PartnerPreference> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MinAnnualIncome)
                .HasPrecision(18, 2);

            builder.Property(x => x.MaxAnnualIncome)
                .HasPrecision(18, 2);

            // User
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<PartnerPreference>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Religion
            builder.HasOne(x => x.Religion)
                .WithMany()
                .HasForeignKey(x => x.ReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Caste
            builder.HasOne(x => x.Caste)
                .WithMany()
                .HasForeignKey(x => x.CasteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mother Tongue
            builder.HasOne(x => x.MotherTongue)
                .WithMany()
                .HasForeignKey(x => x.MotherTongueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Education
            builder.HasOne(x => x.Education)
                .WithMany()
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Occupation
            builder.HasOne(x => x.Occupation)
                .WithMany()
                .HasForeignKey(x => x.OccupationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Country
            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // State
            builder.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // City
            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
