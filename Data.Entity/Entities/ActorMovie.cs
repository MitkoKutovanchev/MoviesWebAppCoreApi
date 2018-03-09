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
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
