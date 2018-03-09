using Data.DB.Helpers.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DB.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(MoviesWebAppDbContext context)
        {
            context.Database.Migrate();

            UserSeed.Seed(context);
            MovieSeeds.Seed(context);
            ActorSeeds.Seed(context);
            ImgUrlSeed.Seed(context);
            ActorMovieSeeds.Seed(context);
        }
    }
}
