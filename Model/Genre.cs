using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = string.Empty;

        // property: connection to Movie-table (when queried)
        // one genre can belong to many movies (list of movies)
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}