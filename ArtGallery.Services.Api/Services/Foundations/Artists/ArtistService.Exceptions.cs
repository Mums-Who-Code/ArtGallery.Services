// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public partial class ArtistService
    {
        private delegate ValueTask<Artist> ReturningArtistFunction();

        private async ValueTask<Artist> TryCatch(ReturningArtistFunction returningArtistFunction)
        {
            try
            {
                return await returningArtistFunction();
            }
            catch (NullArtistException nullArtistException)
            {
                throw CreateAndLogValidationException(nullArtistException);
            }
            catch (InvalidArtistException invalidArtistException)
            {
                throw CreateAndLogValidationException(invalidArtistException);
            }
            catch (SqlException sqlException)
            {
                var failedArtistStorageException =
                    new FailedArtistStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedArtistStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsArtistException =
                    new AlreadyExistsArtistException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsArtistException);
            }
            catch(ForeignKeyConstraintConflictException
                foreignForeignKeyConstraintConflictException)
            {
                var invalidArtistReferenceException =
                    new InvalidArtistReferenceException(
                        foreignForeignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(
                    invalidArtistReferenceException);
            }
        }

        private ArtistValidationException CreateAndLogValidationException(Xeption exception)
        {
            var artistValidationException = new ArtistValidationException(exception);
            this.loggingBroker.LogError(artistValidationException);

            return artistValidationException;
        }

        private ArtistDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var artistDependencyException = new ArtistDependencyException(exception);
            this.loggingBroker.LogCritical(artistDependencyException);

            return artistDependencyException;
        }

        private ArtistDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var artistValidationDependencyException =
                new ArtistDependencyValidationException(exception);

            this.loggingBroker.LogError(artistValidationDependencyException);

            return artistValidationDependencyException;
        }
    }
}
