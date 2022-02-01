// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Brokers.DateTime;
using ArtGallery.Services.Api.Brokers.Loggings;
using ArtGallery.Services.Api.Brokers.Storages;
using ArtGallery.Services.Api.Models.Artists;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public partial class ArtistService : IArtistService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public ArtistService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<Artist> AddArtistAsync(Artist artist) =>
        TryCatch(async () =>
        {
            ValidateInput(artist);

            return await this.storageBroker.InsertArtistAsync(artist);
        });
    }
}
