using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity.Entities
{
    public sealed class User : BaseEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsAdmin { get; set; }
        public List<Movie> WatchedMovies { get; set; }

        public User()
        {
            AvatarUrl = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png";
            IsAdmin = false;
            WatchedMovies = new List<Movie>();
            this._id = null ?? new CustomId();
        }

        public User(string username, string password, string email, string avatarURl, bool isAdmin, CustomId id = null)
        {
            this.Username = username;
            this.Password = password;
            this.EMail = email;
            this.AvatarUrl = avatarURl;
            if (AvatarUrl == null)
            {
                this.AvatarUrl = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png";
            }
            this.IsAdmin = isAdmin;
            this._id = id ?? new CustomId();
        }

        public User(string username, string password, string email, string avatarURl, CustomId id = null)
        {
            this.Username = username;
            this.Password = password;
            this.EMail = email;
            this.AvatarUrl = avatarURl;
            if (AvatarUrl == null)
            {
                this.AvatarUrl = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png";
            }
            this._id = id ?? new CustomId();
        }

    }
}
