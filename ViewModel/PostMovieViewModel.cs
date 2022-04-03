using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.ViewModel
{
    public class PostMovieViewModel
    {
        // Id handled in ListMovieViewModel
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Length { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;        // the FK in Movies-table
        public string ImageUrl { get; set; } = string.Empty;
    }
}