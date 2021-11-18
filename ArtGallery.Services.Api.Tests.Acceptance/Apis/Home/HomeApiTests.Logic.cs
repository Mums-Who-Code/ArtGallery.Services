// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace ArtGallery.Services.Api.Tests.Acceptance.Apis.Home
{
    public partial class HomeApiTests
    {
        [Fact]
        public async Task ShouldReturnHomeMesssageAsync()
        {
            //given
            string expectedHomeMessage =
                "Hello! I am ArtGallery Services";

            //when
            string actualHomeMessage =
               await this.artGalleryApiBroker.GetHomeMessageAsync();

            //then
            actualHomeMessage.Should().BeEquivalentTo(expectedHomeMessage);

        }
    }
}
