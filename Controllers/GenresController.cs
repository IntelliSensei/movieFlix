using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieflix_api.API_Data;
using movieflix_api.Model;
using movieflix_api.ViewModel;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/v1/genres")]    // change to steady end-URL


    // Controller = sets up connection between API and other interfaces (DB, Client etc.)

    public class GenresController : ControllerBase
    {
        private readonly DataContext _context;

        public GenresController(DataContext context)
        {
            _context = context;
        }



        [HttpGet()]
        public async Task<ActionResult<List<GetGenreViewModel>>> ListGenres()
        {
            var response = await _context.Genres.ToListAsync();     // fetch a list of all genres in the DB
            var genres = new List<GetGenreViewModel>();             // create empty list

            foreach (var genre in response)                         // loop through resulting list of genres from DB
            {
                genres.Add(new GetGenreViewModel        // for every genre found, add new instance of GGVM to the empty genres-list
                {
                    Id = genre.Id,                      // data for Id and GenreName (from DB) is mapped to the same properties in GGVM
                    GenreName = genre.GenreName
                });
            }

            return Ok(genres);              // return the completed list (genres)
        }




        [HttpPost()]
        public async Task<ActionResult<Genre>> AddGenre(PostGenreViewModel model)
        {
            var newGenre = new Genre
            {
                GenreName = model.GenreName     // mapping: GenreName exists in both PGVM and Genre-model > link them together
                                                // input/post name to model via end-point URL > flows into PGVM > stored in newGenre > continue below (add/save)
            };

            _context.Genres.Add(newGenre);          // add Genre-object (name of genre) to DB
            await _context.SaveChangesAsync();      // save DB

            return StatusCode(201, newGenre);       // return 201 (confirmation code) + added name
        }
    }
}