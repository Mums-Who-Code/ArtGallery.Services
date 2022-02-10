// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfArtistAlreadyExistsAndLogItAsync()
        {
            // given
            Artist randomArtist = CreateRandomArtist();
            Artist alreadyExistsArtist = randomArtist;
            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            var duplicateKeyException = new DuplicateKeyException(exceptionMessage);

            var alreadyExistsArtistException =
                new AlreadyExistsArtistException(duplicateKeyException);

            var expectedArtistDepdendencyValidationException =
                new ArtistDependencyValidationException(alreadyExistsArtistException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(alreadyExistsArtist);

            // then
            await Assert.ThrowsAsync<ArtistDependencyValidationException>(() =>
                addArtistTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                   expectedArtistDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(alreadyExistsArtist),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            //given
            Artist randomArtist = CreateRandomArtist();
            Artist alreadyExistsArtist = randomArtist;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidArtistReferenceException =
                new InvalidArtistReferenceException(foreignKeyConstraintConflictException);

            var expectedArtistDepdendencyValidationException =
                new ArtistDependencyValidationException(invalidArtistReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(foreignKeyConstraintConflictException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(alreadyExistsArtist);

            //then
            await Assert.ThrowsAsync<ArtistDependencyValidationException>(() =>
                addArtistTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedArtistDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(alreadyExistsArtist),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Artist someArtist = CreateRandomArtist();
            string randomMessage = GetRandomString();
            var databaseUpdateException = new DbUpdateException();

            var failedStorageDependencyException =
                new FailedArtistStorageException(databaseUpdateException);

            var expectedArtistDepdendencyException =
                new ArtistDependencyException(
                    failedStorageDependencyException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            // then
            await Assert.ThrowsAsync<ArtistDependencyException>(() =>
                addArtistTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                   expectedArtistDepdendencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertArtistAsync(someArtist),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Artist someArtist = CreateRandomArtist();
            var serviceException = new Exception();

            var failedArtistServiceException =
                new FailedArtistServiceException(serviceException);

            var expectedArtistServiceException =
                new ArtistServiceException(failedArtistServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(serviceException);

            //when
            ValueTask<Artist> addArtistTask =
                this.artistService.AddArtistAsync(someArtist);

            //then
            await Assert.ThrowsAsync<ArtistServiceException>(() =>
                addArtistTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtistServiceException))),
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