// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Services.Api.Tests.Acceptance.Brokers;
using Xunit;

namespace ArtGallery.Services.Api.Tests.Acceptance.Apis.Home
{
    [Collection(nameof(ApiTestCollection))]
    public partial class HomeApiTests
    {
        private readonly ArtGalleryApiBroker artGalleryApiBroker;
        public HomeApiTests(ArtGalleryApiBroker artGalleryApiBroker) =>
            this.artGalleryApiBroker = artGalleryApiBroker;
    }
}
