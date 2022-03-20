using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.Model
{
    public class Movie
    {
        // properties (matching the json-file):
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Length { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }


        // constructor:
        public Movie()
        {
            Title = string.Empty;   // circumventing non-nullable-issue - by setting an empty/default-value
            Length = string.Empty;
            Director = string.Empty;
            Genre = string.Empty;
            ImageUrl = string.Empty;

        }
    }
}