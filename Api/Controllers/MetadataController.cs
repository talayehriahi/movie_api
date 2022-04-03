using System;
using System.Collections.Generic;
using System.Linq;
using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {

        [HttpGet("{movieId}")]
        public ActionResult<List<MetadataDto>> Get(int movieId, [FromServices] MetadataService metadataService)
        {
            var result = metadataService.Get(movieId);

            if (!result.Any())
                Response.StatusCode = 404;

            return result;
        }

        [HttpPost]
        public void Post([FromBody] MetadataDto metadata, [FromServices] MetadataService metadataService)
        {
            metadataService.Add(metadata);
        }
    }
}
