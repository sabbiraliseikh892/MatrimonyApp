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
    public class UserFavoriteConfiguration
        : IEntityTypeConfiguration<UserFavorite>
    {
        public void Configure(EntityTypeBuilder<UserFavorite> builder)
        {
            builder.ToTable("UserFavorites");

            builder.HasKey(x => x.Id);

            //-------------------------------------------------------
            // User
            //-------------------------------------------------------

            builder.HasOne(x => x.User)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //-------------------------------------------------------
            // Favorite User
            //-------------------------------------------------------

            builder.HasOne(x => x.FavoriteUser)
                .WithMany(x => x.FavoritedByUsers)
                .HasForeignKey(x => x.FavoriteUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //-------------------------------------------------------
            // Prevent Duplicate Favorites
            //-------------------------------------------------------

            builder.HasIndex(x => new
            {
                x.UserId,
                x.FavoriteUserId
            }).IsUnique();

            //-------------------------------------------------------
            // Performance Indexes
            //-------------------------------------------------------

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.FavoriteUserId);

            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
