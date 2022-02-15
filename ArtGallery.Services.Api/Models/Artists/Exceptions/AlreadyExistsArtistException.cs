// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class AlreadyExistsArtistException : Xeption
    {
        public AlreadyExistsArtistException(Exception innerException)
            : base(message: "Artist with id already exists.",
                  innerException)
        { }
    }
}
