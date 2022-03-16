using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public Movie()
        {
            Title = string.Empty;   // circumventing non-nullable-issue - by setting an empty/default-value
        }
    }
}