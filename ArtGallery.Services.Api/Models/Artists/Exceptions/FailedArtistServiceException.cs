// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class FailedArtistServiceException : Xeption
    {
        public FailedArtistServiceException(Exception innerException)
            : base(message: "Failed artist service error occurred.", innerException)
        { }
    }
}
