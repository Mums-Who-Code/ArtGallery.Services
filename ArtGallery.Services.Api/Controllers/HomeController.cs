// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace ArtGallery.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            Ok("Hello! I am ArtGallery Services");
    }
}
