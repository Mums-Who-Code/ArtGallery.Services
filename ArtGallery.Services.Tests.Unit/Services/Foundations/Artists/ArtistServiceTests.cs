// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using ArtGallery.Services.Api.Brokers.DateTime;
using ArtGallery.Services.Api.Brokers.Loggings;
using ArtGallery.Services.Api.Brokers.Storages;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Services.Foundations.Artists;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace ArtGallery.Services.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IArtistService aristService;

        public ArtistServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.aristService = new ArtistService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData InvalidEmails()
        {
            string randomString = GetRandomString();
            string letterString = randomString;
            string characterString = $"\n\r\b{randomString}^8&";
            string domainString = $"{randomString}.com";
            string incompleteEmailString = $"{randomString}@{randomString}";

            return new TheoryData<string>
            {
                null,
                "",
                "  ",
                letterString,
                characterString,
                domainString,
                incompleteEmailString
            };
        }

        public static TheoryData InvalidContactNumbers()
        {
            string randomString = GetRandomString();

            return new TheoryData<string>
            {
                null,
                "",
                "  ",
                randomString
            };
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Artist CreateRandomArtist() =>
            CreateArtistFiller(dateTime: GetRandomDateTime()).Create();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static Filler<Artist> CreateArtistFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Artist>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime);

            return filler;
        }
    }
}
