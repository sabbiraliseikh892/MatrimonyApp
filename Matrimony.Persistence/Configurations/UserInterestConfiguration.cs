using Matrimony.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Persistence.Configurations
{
    public class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder.ToTable("UserInterests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.InitialMessage)
                .HasMaxLength(500);

            builder.Property(x => x.ResponseReason)
                .HasMaxLength(500);

            builder.Property(x => x.Status)
                .HasConversion<int>();

            // Sender
            builder.HasOne(x => x.FromUser)
                .WithMany(x => x.SentInterests)
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Receiver
            builder.HasOne(x => x.ToUser)
                .WithMany(x => x.ReceivedInterests)
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Performance Indexes
            builder.HasIndex(x => new
            {
                x.FromUserId,
                x.ToUserId
            }).IsUnique();

            builder.HasIndex(x => new
            {
                x.FromUserId,
                x.Status
            });

            builder.HasIndex(x => new
            {
                x.ToUserId,
                x.Status
            });

            builder.HasIndex(x => x.CreatedAt);

            // Prevent duplicate interest between same users
            builder.HasIndex(x => new
            {
                x.FromUserId,
                x.ToUserId
            }).IsUnique();
        }
    }
}
