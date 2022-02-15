// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class InvalidArtistReferenceException : Xeption
    {
        public InvalidArtistReferenceException(Exception innerException)
            : base(message: "Invalid artist reference error occurred.", innerException)
        { }
    }
}
