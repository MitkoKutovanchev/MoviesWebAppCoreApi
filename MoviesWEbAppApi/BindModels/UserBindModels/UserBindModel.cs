using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWEbAppApi.BindModels
{
    public class UserBindModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsAdmin { get; set; }
        public List<Movie> WatchedMovies { get; set; }
        public string apiIdPlaceholder { get; set; }
    }
}
