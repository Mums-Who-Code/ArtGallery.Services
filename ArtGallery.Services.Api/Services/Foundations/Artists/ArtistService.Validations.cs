// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public partial class ArtistService
    {
        private void ValidateInput(Artist artist)
        {
            if (artist == null)
            {
                throw new NullArtistException();
            }
        }
    }
}
