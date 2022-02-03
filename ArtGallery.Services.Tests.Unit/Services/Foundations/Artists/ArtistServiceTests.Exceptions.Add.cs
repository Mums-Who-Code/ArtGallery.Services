// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace ArtGallery.Services.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Artist randomArtist = CreateRandomArtist();
            SqlException sqlException = GetSqlException();

            var failedArtistStorageException =
                new FailedArtistStorageException(sqlException);

            var expectedArtistDependencyException =
                new ArtistDependencyException(failedArtistStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                .Throws(sqlException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(randomArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyException>(() =>
                addArtistTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedArtistDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
