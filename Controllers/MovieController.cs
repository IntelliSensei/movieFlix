using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movieflix_api.API_Data;   // link to data-loader
using movieflix_api.Model;      // link to Movie-class


namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]

    public class MovieController : ControllerBase
    {

        [HttpGet()]             // decoration 1
        public async Task<ActionResult<IEnumerable<Movie>>> MoviesList()    // add ActionResult<> to the return ...
                                                                            // ... inside, replace string-type with Movie-type ...
                                                                            // ... wrap everything with Task = Task will return an ActionResult, of type IEnum., containing Movie-type ...
                                                                            // ... Task = new thread to execute a set of code and return everything within Task<> ...
                                                                            // ... used to avoid queue-buildups
        {
            // HARDCODED //
            #region 
            // var movies = new List<Movie>();                             // ... here too
            // movies.Add(new Movie { Id = 1, Title = "Aar ya paar" });    // add new Movie-objects with properties Id and Title to the list
            // movies.Add(new Movie { Id = 2, Title = "Aankhen" });
            #endregion


            // SOFTCODED //

            var movies = await LoadData.LoadMovies();       // load data from LoadData > LoadMovies

            return Ok(movies);                              // add 'Ok' to return
        }


        [HttpGet("{title}")]    // decoration 2 - same as the input parameter
        public ActionResult<Movie> MyMovie(string title)
        {
            var movie = new Movie { Id = 10, Title = "Haider" };
            return Ok(movie);
        }

    }
}