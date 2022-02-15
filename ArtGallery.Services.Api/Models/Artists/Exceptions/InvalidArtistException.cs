// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace ArtGallery.Services.Api.Models.Artists.Exceptions
{
    public class InvalidArtistException : Xeption
    {
        public InvalidArtistException()
            : base(message: "Invalid artist, fix the errors and try again.")
        { }
    }
}
