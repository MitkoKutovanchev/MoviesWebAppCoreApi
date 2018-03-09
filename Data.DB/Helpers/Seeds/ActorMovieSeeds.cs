using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DB.Helpers.Seeds
{
    public class ActorMovieSeeds
    {
        public static void Seed(MoviesWebAppDbContext context)
        {
            if (context.ActorMovies.Any()) return;
            foreach (var ActorMovie in GetInitialActorMovies())
            {
                context.Add(ActorMovie);
            }
            context.SaveChanges();
        }

        private static List<ActorMovie> GetInitialActorMovies()
        {
            List<Actor> actors = ActorSeeds.GetInitialActors();
            List<Movie> movies = MovieSeeds.GetInitialMovies();
            return new List<ActorMovie>
            {
                new ActorMovie {Actor = actors[0], Movie = movies[0]},
                new ActorMovie {Actor = actors[1], Movie = movies[0]},
                new ActorMovie {Actor = actors[2], Movie = movies[0]},
                new ActorMovie {Actor = actors[3], Movie = movies[0]},

                new ActorMovie {Actor = actors[4], Movie = movies[1]},
                new ActorMovie {Actor = actors[5], Movie = movies[1]},
                new ActorMovie {Actor = actors[6], Movie = movies[1]},
                new ActorMovie {Actor = actors[7], Movie = movies[1]},

                new ActorMovie {Actor = actors[8], Movie = movies[2]},
                new ActorMovie {Actor = actors[9], Movie = movies[2]},
                new ActorMovie {Actor = actors[10], Movie = movies[2]},
                new ActorMovie {Actor = actors[11], Movie = movies[2]}
            };
        }
    }
}
