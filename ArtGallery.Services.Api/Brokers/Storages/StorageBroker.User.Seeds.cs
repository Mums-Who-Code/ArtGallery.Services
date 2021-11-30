// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Services.Api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var serviceAdminUser = new User
            {
                Id = System.Guid.Parse("c3a817cb-1528-4e30-81c0-389297978dc8"),
                Name = "Admin"
            };

            modelBuilder.Entity<User>().HasData(serviceAdminUser);
        }
    }
}
