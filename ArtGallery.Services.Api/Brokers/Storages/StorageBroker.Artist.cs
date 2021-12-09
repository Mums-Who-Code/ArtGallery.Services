// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ArtGallery.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Artist> Artists { get; set; }

        public async ValueTask<Artist> InsertArtistAsync(Artist artist)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Artist> entityEntry =
                await broker.Artists.AddAsync(artist);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
