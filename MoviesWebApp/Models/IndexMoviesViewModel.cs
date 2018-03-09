using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp.Models
{
    public class IndexMoviesViewModel
    {
        public List<Movie> moviesForVm { get; set; }
    }
}
