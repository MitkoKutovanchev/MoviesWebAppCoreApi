using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels.MovieBindModels;
using MoviesWEbAppApi.wwwroot.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {

        MovieRepository movieRepo = new MovieRepository();
        ActorRepository actorRepo = new ActorRepository();
        ActorMovieRepository actorMovieRepo = new ActorMovieRepository();

        //Get All Movies
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<MovieBindModel> moviesModel = new List<MovieBindModel>();

            foreach (var movie in movieRepo.GetAll())
            {
                moviesModel.Add(new MovieBindModel
                {
                    Name = movie.Name,
                    ReleaseDate = movie.ReleaseDate,
                    imgUrl = movie.imgUrl,
                    MovieIMDBScore = movie.MovieIMDBScore,
                    MovieIMDBUrl = movie.MovieIMDBUrl,
                    MovieRottenTomatoesScore = movie.MovieRottenTomatoesScore,
                    MovieRottenTomatoesUrl = movie.MovieRottenTomatoesUrl,
                    MovieDesc = movie.MovieDesc
                });
            }
            return Ok(moviesModel);
        }
        // Get Movie By Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }
            return Ok(movieRepo.Get(a => a.Id == id));
        }
        //Add Movie
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]MovieBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            Movie movie = new Movie();
            movie.Name = model.Name;
            movie.ReleaseDate = model.ReleaseDate;
            movie.imgUrl = model.imgUrl;
            movie.MovieIMDBScore = model.MovieIMDBScore;
            movie.MovieIMDBUrl = model.MovieIMDBUrl;
            movie.MovieRottenTomatoesScore = model.MovieRottenTomatoesScore;
            movie.MovieRottenTomatoesUrl = model.MovieRottenTomatoesUrl;
            movie.MovieDesc = model.MovieDesc;

            movieRepo.Insert(movie);

            return Ok(movie);

        }
        //Edit Movie
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]MovieBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            Movie movie = movieRepo.Get(a => a.Id == id);
            movie.Name = model.Name;
            movie.ReleaseDate = model.ReleaseDate;
            movie.imgUrl = model.imgUrl;
            movie.MovieIMDBScore = model.MovieIMDBScore;
            movie.MovieIMDBUrl = model.MovieIMDBUrl;
            movie.MovieRottenTomatoesScore = model.MovieRottenTomatoesScore;
            movie.MovieRottenTomatoesUrl = model.MovieRottenTomatoesUrl;
            movie.MovieDesc = model.MovieDesc;

            movieRepo.Update(movie);

            return Ok(movieRepo);

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }
            Movie movie = movieRepo.Get(a => a.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            movieRepo.Delete(movie);

            return Ok(movie);
        }

        //Add an actor to the movie
        [HttpPut("{idM}/actors/{idA}")]
        public IActionResult addActorToMovie(string idM, string idA)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            if (movieRepo.Get(a => a.Id == idM) == null || actorRepo.Get(a => a.Id == idA) == null)
            {
                return NotFound();
            }

            ActorMovie actorMovie = new ActorMovie();
            actorMovie.Actor = actorRepo.Get(a => a.Id == idA);
            actorMovie.Movie = movieRepo.Get(a => a.Id == idM);

            actorMovieRepo.Insert(actorMovie);

            return Ok(actorMovie);
        }

        [HttpGet("{id}/actors")]
        public IActionResult viewActorsForMovie(string id)
        {
            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            List<Actor> actorsList = new List<Actor>();

            foreach (var actorMovie in movieRepo.Get(a => a.Id == id).Actors)
            {

                Actor actor = actorRepo.Get(a => a.Id == actorMovie.ActorId);
                actorsList.Add(actor);
            }

            return Ok(actorsList);

        }
    }
}
