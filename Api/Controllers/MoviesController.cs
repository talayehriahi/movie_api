using System;
using System.Collections.Generic;
using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("stats")]
        public ActionResult<List<StatsDto>> GetStats([FromServices] StatsService statsService)
        {
            var result = statsService.GetAll();

            return result;
        }
    }
}
