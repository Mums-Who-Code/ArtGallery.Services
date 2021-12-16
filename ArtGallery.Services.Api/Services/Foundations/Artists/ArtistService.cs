// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Brokers.Storages;
using ArtGallery.Services.Api.Models.Artists;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public class ArtistService : IArtistService
    {
        private readonly IStorageBroker storageBroker;

        public ArtistService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Artist> AddArtistAsync(Artist artist) =>
            throw new System.NotImplementedException();
    }
}
