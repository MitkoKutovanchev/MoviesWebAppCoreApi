using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWEbAppApi.BindModels.MovieBindModels
{
    public class MovieViewModel : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImgUrl { get; set; }
        public double MovieIMDBScore { get; set; }
        public string MovieIMDBUrl { get; set; }
        public double MovieRottenTomatoesScore { get; set; }
        public string MovieRottenTomatoesUrl { get; set; }
        public string MovieDesc { get; set; }
    }
}
