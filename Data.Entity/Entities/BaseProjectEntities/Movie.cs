using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.Entities
{
    public class Movie : BaseEntity
    {

        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImgUrl { get; set; }
        public double MovieIMDBScore { get; set; }
        public string MovieIMDBUrl { get; set; }
        public double MovieRottenTomatoesScore { get; set; }
        public string MovieRottenTomatoesUrl { get; set; }
        public string MovieDesc{ get; set; }


        public virtual ICollection<ActorMovie> Actors { get; set; }
        public ICollection<ImgUrls> aditionalImgUrls { get; set; }

        public Movie()
        {
            this._id = null ?? new CustomId();
        }

        public Movie(string name, DateTime releaseDate, string imgURL, double iMDBScore, string iMDBUrl, double rottenTomatoesScore, string rottenTomatoesUrl, string description, CustomId id = null)
        {
            this.Name = name;
            this.ReleaseDate = releaseDate;
            this.ImgUrl = ImgUrl;
            this.MovieIMDBScore = iMDBScore;
            this.MovieIMDBUrl = iMDBUrl;
            this.MovieRottenTomatoesScore = rottenTomatoesScore;
            this.MovieRottenTomatoesUrl = rottenTomatoesUrl;
            this.MovieDesc = description;
            this._id = id ?? new CustomId();
        }
    }
}
