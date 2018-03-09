using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DB.Helpers.Seeds
{
    class ImgUrlSeed
    {
        public static void Seed(MoviesWebAppDbContext context)
        {
            if (context.Urls.Any()) return;
            foreach (var Url in GetInitialUrls())
            {
                context.Add(Url);
            }
            context.SaveChanges();
        }

        public static List<ImgUrls> GetInitialUrls()
        {
            List<Movie> movies = MovieSeeds.GetInitialMovies();
            return new List<ImgUrls>
            {
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMjQzNTM2MzEwOF5BMl5BanBnXkFtZTgwOTE3MDM5NDM@._V1_SX1777_CR0,0,1777,937_AL_.jpg", movies[0]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMTg3NTc5MzA0OV5BMl5BanBnXkFtZTgwMDI3MDM5NDM@._V1_SX1777_CR0,0,1777,937_AL_.jpg", movies[0]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMTU2MTE1MjgwOV5BMl5BanBnXkFtZTgwMTI3MDM5NDM@._V1_SX1500_CR0,0,1500,999_AL_.jpg", movies[0]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMjI4NzM0MDI3NF5BMl5BanBnXkFtZTgwNDI3MDM5NDM@._V1_SX1777_CR0,0,1777,937_AL_.jpg", movies[0]),
                
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BNDFlOWQwNjAtZjRjMC00Mjg0LWJiOGUtY2FkYmQ4OGQ3MzY0XkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[1]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BOWU3OGJjZjEtN2JkYS00MTZmLTkyY2EtYzY4OTIwZTlkM2Q1XkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[1]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMjA0MDQ3ZWUtMTdhNS00ODFlLTgxZmEtZWM5ZmQxMTU3ZDdmXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[1]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BMmIzM2I5NjgtZjQ2Zi00NWY3LWI0YTItMTYyMTY0ODA1OWZkXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[1]),


                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BODUwMDk4NzQtYTVjYy00ODdjLTllOTgtOTFjY2MzNjk5NWMzXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[2]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BNDNhZDdmMjUtNGMyYy00NjFkLWEzYTktYzdhODgzYjE1ZDBjXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_SX1500_AL_.jpg", movies[2]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BNjBhOTQ4YzctN2VjNy00ZWQ1LWI4YTgtZTdjZjY3NTZjNzRlXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_CR0,0,1497,1000_AL_.jpg", movies[2]),
                new ImgUrls("https://images-na.ssl-images-amazon.com/images/M/MV5BZTdhMGVmMDMtODdlOC00OTlkLWI3ZjctZjlmMzc2M2MxZGRiXkEyXkFqcGdeQXVyNDg2MjUxNjM@._V1_SY1000_SX1500_AL_.jpg", movies[2])
            };
    }
}}
