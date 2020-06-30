using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheMovieDB.Common.DTOs;
using TheMovieDB.Core.Interfaces;

namespace TheMovieDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieBO _movie;

        public MovieController(IMovieBO BO)
        {
            this._movie = BO;
        }

        [HttpGet]
        public IList<MovieDTO> Get()
        {
            return _movie.List();
        }
    }
}
