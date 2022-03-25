using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;    // add EF Core
using movieflix_api.API_Data;
using movieflix_api.Model;

namespace movieflix_api.API_Data
{
    public class DataContext : DbContext
    {

        // property (special with lambda): fetch table-structure (Movie.cs)
        // avoid nullable-issue: lambda-expression = if 'Movies' is null ...
        // ... set Movie-object to empty and return it as empty object (non-null)
        public DbSet<Movie> Movies => Set<Movie>();


        // constructor: input-parameter 'options' (connection strings)
        // contains resources needed to run the objects
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}