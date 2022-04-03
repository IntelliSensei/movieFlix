using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieflix_api.ViewModel
{

    // GGVM (below) = a separate definition of the Genre-model ..
    // .. to avoid sending/returning the same model used to create data in the DB

    public class GetGenreViewModel
    {
        public int Id { get; set; }
        public string GenreName { get; set; } = string.Empty;
    }
}