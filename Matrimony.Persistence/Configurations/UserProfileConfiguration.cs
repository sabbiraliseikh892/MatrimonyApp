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
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(
        EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProfileId)
                .HasMaxLength(20);

            builder.HasIndex(x => x.ProfileId);

            builder.HasIndex(x => x.City);

            builder.HasIndex(x => x.Religion);

            builder.HasIndex(x => x.Caste);
        }
    }
}
