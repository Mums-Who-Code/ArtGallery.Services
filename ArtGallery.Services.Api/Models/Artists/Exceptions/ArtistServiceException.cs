// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class ArtistServiceException : Xeption
    {
        public ArtistServiceException(Xeption innerException)
            : base(message: "Artist service error occured, contact support.", innerException)
        { }
    }
}
