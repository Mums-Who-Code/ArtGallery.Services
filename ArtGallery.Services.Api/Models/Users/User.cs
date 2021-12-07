// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ArtGallery.Services.Api.Models.Artists;
using Newtonsoft.Json;

namespace ArtGallery.Services.Api.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<Artist> CreatedArtists { get; set; }

        [JsonIgnore]
        public IEnumerable<Artist> UpdatedArtists { get; set; }
    }
}
