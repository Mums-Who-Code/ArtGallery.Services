// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;
using ArtGallery.Services.Api.Services.Foundations.Artists;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace ArtGallery.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : RESTFulController
    {
        private readonly IArtistService artistService;

        public ArtistController(IArtistService artistService) =>
            this.artistService = artistService;

        [HttpPost]
        public async ValueTask<ActionResult<Artist>> PostArtistAsync(Artist artist)
        {
            try
            {
                Artist createdArtist =
                    await this.artistService.AddArtistAsync(artist);

                return Created(createdArtist);
            }
            catch (ArtistValidationException artistValidationException)
            {
                return BadRequest(artistValidationException.InnerException);
            }
            catch (ArtistDependencyException artistDependencyException)
            {
                return InternalServerError(artistDependencyException);
            }
            catch (ArtistDependencyValidationException artistDependencyValidationException)
                when(artistDependencyValidationException.InnerException is AlreadyExistsArtistException)
            {
                return Conflict(artistDependencyValidationException.InnerException);
            }
            catch (ArtistDependencyValidationException artistDependencyValidationException)
                when(artistDependencyValidationException.InnerException is InvalidArtistReferenceException)
            {
                return  FailedDependency(artistDependencyValidationException.InnerException);
            }
            catch (ArtistServiceException artistServiceException)
            {
                return InternalServerError(artistServiceException);
            }
        }
    }
}
