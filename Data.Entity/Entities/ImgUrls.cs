using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity.Entities
{
    public class ImgUrls : BaseEntity
    {

        public string Url { get; set; }

        public Movie Movie { get; set; }

        public ImgUrls()
        {

        }

        public ImgUrls(string url, Movie movie)
        {
            this.Url = url;
            this.Movie = movie;
        }
    }
}
