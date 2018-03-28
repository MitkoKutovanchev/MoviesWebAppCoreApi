using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DB.Helpers.Seeds
{
    public class MovieSeeds
    {
        public static void Seed(MoviesWebAppDbContext context)
        {
            if (context.Movies.Any()) return;
            foreach (var Movie in GetInitialMovies())
            {
                context.Movies.Add(Movie);
            }
            context.SaveChanges();
        }

        public static List<Movie> GetInitialMovies()
        {
            return new List<Movie>
            {
                new Movie("Black Panther (2018)", 
                DateTime.Parse("16-02-2018"),
                "https://images-na.ssl-images-amazon.com/images/M/MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_SY1000_CR0,0,674,1000_AL_.jpg", 
                7.8, 
                "http://www.imdb.com/title/tt1825683/?ref_=inth_ov_i", 
                8.2, 
                "https://www.rottentomatoes.com/m/black_panther_2018",
                "T'Challa, the King of Wakanda, rises to the throne in the isolated, technologically advanced African nation, but his claim is challenged by a vengeful outsider who was a childhood victim of T'Challa's father's mistake."),


                new Movie("Red Sparrow (2018)", 
                DateTime.Parse("02.03.2018"), 
                "https://images-na.ssl-images-amazon.com/images/M/MV5BMTA3MDkxOTc4NDdeQTJeQWpwZ15BbWU4MDAxNzgyNTQz._V1_SY1000_CR0,0,674,1000_AL_.jpg", 
                6.7, "http://www.imdb.com/title/tt2873282/?ref_=inth_ov_i", 
                5.6, 
                "https://www.rottentomatoes.com/m/red_sparrow",
                "Ballerina Dominika Egorova is recruited to 'Sparrow School,' a Russian intelligence service where she is forced to use her body as a weapon. Her first mission, targeting a C.I.A. agent, threatens to unravel the security of both nations."),


                new Movie("Death Wish (2018)", 
                DateTime.Parse("02.03.2018"), 
                "https://images-na.ssl-images-amazon.com/images/M/MV5BMTkzNjU3MjE0MF5BMl5BanBnXkFtZTgwNTIyNDk0NDM@._V1_.jpg", 
                6.8, 
                "http://www.imdb.com/title/tt1137450/?ref_=inth_ov_i", 
                3.7, 
                "https://www.rottentomatoes.com/m/death_wish_2018",
                "Dr. Paul Kersey is an experienced trauma surgeon, a man who has spent his life saving lives. After an attack on his family, Paul embarks on his own mission for justice.")
            };
        }
    }
}
