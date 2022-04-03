using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.ViewModel
{
    // this extra model-class is created to add/return (post or get) correct data from/to end-points ..
    // .. reason: never use the exact models (Movie, Genre) which are used to create data in the DB ..
    // .. instead create a "copy" of the models to be used for sending/returning data

    public class PostGenreViewModel
    {
        public string GenreName { get; set; } = string.Empty;
    }
}