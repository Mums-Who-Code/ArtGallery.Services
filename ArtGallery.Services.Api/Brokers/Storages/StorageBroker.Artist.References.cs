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
            modelBuilder.Entity<Artist>()
                .HasOne(artist => artist.CreatedByUser)
                .WithMany(user => user.CreatedArtists)
                .HasForeignKey(artist => artist.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Artist>()
                .HasOne(artist => artist.UpdatedByUser)
                .WithMany(user => user.UpdatedArtists)
                .HasForeignKey(artist => artist.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
