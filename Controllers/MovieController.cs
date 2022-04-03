using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieflix_api.API_Data;   // link to data-loader
using movieflix_api.Model;      // link to Movie-class
using movieflix_api.ViewModel;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]

    public class MovieController : ControllerBase
    {

        private readonly DataContext _context;      // field

        public MovieController(DataContext context) // ctor that sets its in-parameter equal to field (use anywhere in the class) ...
                                                    // ... each class-instance MUST take this in-parameter (DB connection)
        {
            _context = context;                     // dependency injection = to instantiate a class-object, need a DB-connection
        }


        #region HttpGet (basic) 
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

            //var movies = await LoadData.LoadMovies();         // load data from LoadData > LoadMovies

            var movies = await _context.Movies.ToListAsync();   // fetch data from the Azure DB (not locally)  

            return Ok(movies);                                  // add 'Ok' to return
        }
        #endregion

        #region HttpGet + parameter (basic)
        [HttpGet("{title}")]    // decoration 2 - same as the input parameter
        public ActionResult<Movie> MyMovie(string title)
        {
            var movie = new Movie { Id = 10, Title = "Haider" };
            return Ok(movie);
        }
        #endregion


        [HttpGet()]     // fetch list of movies from DB
        public async Task<ActionResult<List<ListMovieViewModel>>> ListMovies()
        {
            var movies = await _context.Movies.ToListAsync();   // list all returned movies from DB
            var movieList = new List<ListMovieViewModel>();     // create empty list

            foreach (var movie in movies)
            {
                movieList.Add(new ListMovieViewModel            // add each looped movie to the empty list
                                                                // property-mapping: each movie from DB (movie) vs LMVM (proper data return)
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear,
                    Length = movie.Length,
                    Director = movie.Director,
                    Genre = movie.Genre.GenreName,
                    ImageUrl = movie.ImageUrl
                });
            }

            return Ok(movieList);       // return completed movie-list
        }


        [HttpPost()]    // add a movie to DB
        public async Task<ActionResult<ListMovieViewModel>> AddMovie(PostMovieViewModel model)
        {
            var newMovie = new Movie    // mapping: model vs PMVM-model
            {
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Length = model.Length,
                Director = model.Director,
                ImageUrl = model.ImageUrl
            };


            // genre-property handled separately ..
            // .. genre must exists in the Genre-table - to enable adding it to a new movie

            var genre = await _context.Genres.FirstOrDefaultAsync(c => c.GenreName == model.Genre);     // FODA = if the genre-string passed in during new movie-creation matches with genre existing in DB ..
                                                                                                        // .. return first instance of it, else, return default (null)

            if (genre == null)      // if genre doesn't exist (null), post error code (unable to create new movie)
            {
                return NotFound($"Could not find genre called: {model.Genre}");
            }


            // else                 // if genre exists in Genre-table in DB, add new movie with all properties to DB, and save DB

            newMovie.Genre = genre;                         // mapping continuation: model vs PMVM-model
            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            var responseModel = new ListMovieViewModel      // mapping: saved movie in DB (newMovie) vs LMVM (needed to post in proper format (HttpPost))
            {
                MovieId = newMovie.Id,
                Title = newMovie.Title,
                ReleaseYear = newMovie.ReleaseYear,
                Length = newMovie.Length,
                Director = newMovie.Director,
                Genre = newMovie.Genre.GenreName,
                ImageUrl = newMovie.ImageUrl
            };


            return StatusCode(201, responseModel);
        }
    }
}