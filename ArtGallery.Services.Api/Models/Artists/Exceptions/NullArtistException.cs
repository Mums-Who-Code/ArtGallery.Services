// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class NullArtistException : Xeption
    {
        public NullArtistException()
            : base(message: "The artist is null.")
        { }
    }
}
