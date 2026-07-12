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
    public class NotificationConfiguration
       : IEntityTypeConfiguration<Notification>
    {
        public void Configure(
            EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Data)
                .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.IsRead);

            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
