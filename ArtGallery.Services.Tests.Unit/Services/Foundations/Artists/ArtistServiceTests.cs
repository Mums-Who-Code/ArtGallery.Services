// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using ArtGallery.Services.Api.Brokers.Storages;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Services.Foundations.Artists;
using Moq;
using Tynamix.ObjectFiller;

namespace ArtGallery.Services.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IArtistService aristService;

        public ArtistServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.aristService = new ArtistService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Artist CreateRandomArtist() =>
            CreateArtistFiller(dateTime: GetRandomDateTime()).Create();

        private static Filler<Artist> CreateArtistFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Artist>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime);

            return filler;
        }
    }
}
