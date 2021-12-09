// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;

namespace ArtGallery.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Artist> InsertArtistAsync(Artist artist);
    }
}
