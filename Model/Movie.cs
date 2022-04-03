using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        //public string Genre { get; set; }     // genre-table created and joined separately (avoid duplicates)

        public int GenreId { get; set; }        // the FK in Movies-table
        public string ImageUrl { get; set; }


        // Navigation property = joining with other tables (DbSets/entities)
        // virtual = property is needed, but will be loaded/queried separately
        // decoration = create and point out the FK in the Movie-table (linked to Genre-table)

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }


        // constructor:
        public Movie()
        {
            Title = string.Empty;   // circumventing non-nullable-issue - by setting an empty/default-value
            Length = string.Empty;
            Director = string.Empty;
            Genre = new Genre();
            ImageUrl = string.Empty;

        }

    }
}