using Data.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DB.Helpers.Seeds
{
    public class UserSeed
    {
        public static void Seed(MoviesWebAppDbContext context)
        {
            if (context.Users.Any()) return;
            foreach (var user in GetInitialUsers())
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }

        private static List<User> GetInitialUsers()
        {
            return new List<User>(){
                new User("admin", "admin", "admin@gmail.com", null, true),
                new User("user", "user", "user@gmail.com", "https://semantic-ui.com/images/avatar/large/elliot.jpg", false),
                new User("anotherUser", "anotherUser", "anotherUser@gmail.com","https://avatarfiles.alphacoders.com/752/75205.png", false)
            };
        }
    }
}
