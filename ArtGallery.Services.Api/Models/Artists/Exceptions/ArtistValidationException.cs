// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class ArtistValidationException : Xeption
    {
        public ArtistValidationException(Xeption innerException)
            : base(message: "Artist validation error occurred.", innerException)
        { }
    }
}
