using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DB.Helpers.Seeds
{
    public class ActorSeeds
    {
        public static void Seed(MoviesWebAppDbContext context)
        {
            if (context.Actors.Any()) return;
            foreach (var Actor in GetInitialActors())
            {
                context.Actors.Add(Actor);
            }
            context.SaveChanges();
        }

        public static List<Actor> GetInitialActors()
        {
            return new List<Actor>
            {
                new Actor("Chadwick", "Boseman"),
                new Actor("Michae", "B. Jordan"),
                new Actor("Lupita", "Nyong'o"),
                new Actor("Danai", "Gurira"),

                new Actor("Ryan", "Coogler"),
                new Actor("Joe", "Robert Cole"),
                new Actor("Stan", "Lee"),
                new Actor("Jack", "Kirby"),

                new Actor("Bruce", "Willis"),
                new Actor("Vincent", "D'Onofrio"),
                new Actor("Elisabeth", "Shue"),
                new Actor("Camila", "Morrone")
            };
        }
    }
}
