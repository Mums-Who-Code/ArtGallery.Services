// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class ArtistValidationException : Xeption
    {
        public ArtistValidationException(Exception innerException)
            : base(message: "Invalid input, contact support.", innerException)
        { }
    }
}
