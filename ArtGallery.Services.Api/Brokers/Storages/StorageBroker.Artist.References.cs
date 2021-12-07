// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Services.Api.Models.Artists;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetArtistReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>();
            modelBuilder.Entity<Artist>()
                .HasOne(Artist => Artist.CreatedByUser)
                .WithMany(User => User.CreatedArtists)
                .HasForeignKey(Artist => Artist.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artist>()
                .HasOne(Artist => Artist.UpdatedByUser)
                .WithMany(User => User.UpdatedArtists)
                .HasForeignKey(Artist => Artist.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
