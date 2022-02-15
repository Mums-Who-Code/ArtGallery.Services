// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class ArtistDependencyValidationException : Xeption
    {
        public ArtistDependencyValidationException(Xeption innerException)
            : base(message: "Artist dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
