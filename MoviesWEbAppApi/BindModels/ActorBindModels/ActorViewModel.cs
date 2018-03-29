using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWEbAppApi.BindModels
{
    public class ActorViewModel : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
