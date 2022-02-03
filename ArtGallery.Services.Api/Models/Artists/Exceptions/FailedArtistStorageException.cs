// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class FailedArtistStorageException : Xeption
    {
        public FailedArtistStorageException(Exception innerException)
            : base(message: "Failed Artist Storage error occured.", innerException)
        { }
    }
}
