// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public interface IArtistService
    {
        ValueTask<Artist> AddArtistAsync(Artist artist);
    }
}
