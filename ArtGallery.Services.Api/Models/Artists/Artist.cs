// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using ArtGallery.Services.Api.Models.Users;

namespace ArtGallery.Services.Api.Models.Artists
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public ArtistStatus Status { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
