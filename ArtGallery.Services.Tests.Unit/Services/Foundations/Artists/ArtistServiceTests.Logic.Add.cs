// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace ArtGallery.Services.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        [Fact]
        public async Task ShouldAddArtistAsync()
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();

            Artist randomArtist = CreateRandomArtist(randomDateTime);
            Artist inputArtist = randomArtist;
            Artist persistedArtist = inputArtist;
            Artist expectedArtist = persistedArtist.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
               broker.GetCurrentDateTime())
                   .Returns(randomDateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertArtistAsync(inputArtist))
                    .ReturnsAsync(persistedArtist);

            //when
            Artist actualArtist =
                await this.artistService.AddArtistAsync(inputArtist);

            //then
            actualArtist.Should().BeEquivalentTo(expectedArtist);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(inputArtist),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
