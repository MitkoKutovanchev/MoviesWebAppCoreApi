using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity.Entities
{
    public class Actor : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ActorMovie> Movies { get; set; }

        public Actor()
        {

        }

        public Actor(string fname, string lname, CustomId id = null)
        {
            this.FirstName = fname;
            this.LastName = lname;
            this._id = id ?? new CustomId();
        }


    }
}
