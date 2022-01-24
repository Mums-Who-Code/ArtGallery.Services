// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;
using Moq;
using Xunit;

namespace ArtGallery.Services.Tests.Unit.Services.Foundations.Artists
{
    public partial class ArtistServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistIsNullAndLogItAsync()
        {
            //given
            Artist nullArtist = null;
            var nullArtistException = new NullArtistException();

            var expectedArtistValidationException =
               new ArtistValidationException(nullArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.aristService.AddArtistAsync(nullArtist);

            //then
            await Assert.ThrowsAsync<ArtistValidationException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedArtistValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfArtistIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given
            var invalidArtist = new Artist
            {
                FirstName = invalidText,
                LastName = invalidText,
                Status = ArtistStatus.InActive
            };

            var invalidArtistException = new InvalidArtistException();

            invalidArtistException.AddData(
                key: nameof(Artist.Id),
                values: "Id is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.FirstName),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.LastName),
                values: "Text is required.");

            invalidArtistException.AddData(
                key: nameof(Artist.Status),
                values: "Value is invalid.");

            invalidArtistException.AddData(
               key: nameof(Artist.CreatedBy),
               values: "Id is required.");

            invalidArtistException.AddData(
               key: nameof(Artist.UpdatedBy),
               values: "Id is required.");

            invalidArtistException.AddData(
               key: nameof(Artist.CreatedDate),
               values: "Date is required.");

            invalidArtistException.AddData(
               key: nameof(Artist.UpdatedDate),
               values: "Date is required.");

            var expectedArtistValidationException =
               new ArtistValidationException(invalidArtistException);

            //when
            ValueTask<Artist> addArtistTask =
                this.aristService.AddArtistAsync(invalidArtist);

            //then
            await Assert.ThrowsAsync<ArtistValidationException>(() =>
                addArtistTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(It.IsAny<Artist>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
