using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity.Entities
{
    public class ActorMovie : BaseEntity
    {

        public ActorMovie()
        {

        }

        public ActorMovie(Actor actor, Movie movie, CustomId id = null)
        {
            this.ActorId = actor.Id;
            this.Actor = actor;

            this.MovieId = movie.Id;
            this.Movie = movie;

            this._id = id ?? new CustomId();
        }

        public String ActorId { get; set; }
        public Actor Actor { get; set; }

        public String MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
