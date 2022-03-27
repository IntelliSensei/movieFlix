using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;         // if JsonSerializer is red-marked: ctrl + . and choose "using ...."
using movieflix_api.Model;      // reference Model-folder with the Movie-class-file
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace movieflix_api.API_Data
{
    public class LoadData
    {
        public static async Task LoadMovies(DataContext context)    // run this as soon as the app starts (Program.cs)
                                                                    // do not return anything, instead push data to Azure DB
        {
            // when LoadMovies is called, perform below data-checks:


            // if DB already contains data, only return (don't load any data):

            if (await context.Movies.AnyAsync())
            {
                return;
            }


            // else (DB doesn't contain data), follow below to add data to DB:

            var jsonCase = new JsonSerializerOptions        // when we run the Serializer (below), ignore case-sensitivity
            {
                PropertyNameCaseInsensitive = true
            };

            var data = await File.ReadAllTextAsync("API_Data/movies.json");         // read and return json-data from the movie-file + its location (use "")
            var movies = JsonSerializer.Deserialize<List<Movie>>(data, jsonCase);   // read the json-data + case > convert it into a list of type Movie (class)


            if (movies is not null)     // if list from above contains data (Movie-model) ...
            {
                await context.AddRangeAsync(movies);    // ... add data to the context-object ...
                await context.SaveChangesAsync();       // ... then save everything to the DB (in one go)
            }


            //return new List<Movie>();   // ... if data is not present, return an empty list
        }

    }
}