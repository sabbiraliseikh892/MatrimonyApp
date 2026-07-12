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
    public class UserProfileViewConfiguration
       : IEntityTypeConfiguration<UserProfileView>
    {
        public void Configure(EntityTypeBuilder<UserProfileView> builder)
        {
            builder.ToTable("UserProfileViews");

            builder.HasKey(x => x.Id);

            //-------------------------------------------------------
            // ViewedAt
            //-------------------------------------------------------

            builder.Property(x => x.ViewedAt)
                   .IsRequired();

            //-------------------------------------------------------
            // Viewer User
            //-------------------------------------------------------

            builder.HasOne(x => x.ViewerUser)
                .WithMany(x => x.ViewedProfiles)
                .HasForeignKey(x => x.ViewerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //-------------------------------------------------------
            // Viewed User
            //-------------------------------------------------------

            builder.HasOne(x => x.ViewedUser)
                .WithMany(x => x.ViewedByUsers)
                .HasForeignKey(x => x.ViewedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //-------------------------------------------------------
            // Prevent duplicate records
            //-------------------------------------------------------

            builder.HasIndex(x => new
            {
                x.ViewerUserId,
                x.ViewedUserId
            }).IsUnique();

            //-------------------------------------------------------
            // Performance Indexes
            //-------------------------------------------------------

            builder.HasIndex(x => x.ViewerUserId);

            builder.HasIndex(x => x.ViewedUserId);

            builder.HasIndex(x => x.ViewedAt);
        }
    }
}
