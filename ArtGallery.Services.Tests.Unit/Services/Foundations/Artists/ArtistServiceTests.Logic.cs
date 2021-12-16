// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
            Artist randomArtist = CreateRandomArtist();
            Artist inputArtist = randomArtist;
            Artist persistedArtist = inputArtist;
            Artist expectedArtist = persistedArtist.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertArtistAsync(inputArtist))
                    .ReturnsAsync(persistedArtist);

            //when
            Artist actualArtist =
                await this.aristService.AddArtistAsync(inputArtist);

            //then
            actualArtist.Should().BeEquivalentTo(expectedArtist);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(inputArtist),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
