using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;         // if JsonSerializer is red-marked: ctrl + . and choose "using ...."
using movieflix_api.Model;      // reference Model-folder with the Movie-class-file
using System.Threading.Tasks;


namespace movieflix_api.API_Data
{
    public class LoadData
    {
        public static async Task<IEnumerable<Movie>> LoadMovies()
        {

            var jsonCase = new JsonSerializerOptions        // when we run the Serializer (below), ignore case-sensitivity
            {
                PropertyNameCaseInsensitive = true
            };

            var data = await File.ReadAllTextAsync("API_Data/movies.json");         // read and return json-data from the movie-file + its location (use "")
            var movies = JsonSerializer.Deserialize<List<Movie>>(data, jsonCase);   // read the json-data + case > convert it into a list of type Movie (class)


            if (movies is not null)     // avoid null-issues, only return if data is present ...
            {
                return movies;
            }

            return new List<Movie>();   // ... if data is not present, return an empty list
        }

    }
}