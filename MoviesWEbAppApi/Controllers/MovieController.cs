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
        public IActionResult Get(int id)
        {
            if (movieRepo.Get(id) == null)
            {
                return NotFound();
            }
            return Ok(movieRepo.Get(id));
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
        public IActionResult Put(int id, [FromBody]MovieBindModel model)
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

            if(movieRepo.Get(id) == null)
            {
                return NotFound();
            }

            Movie movie = movieRepo.Get(id);
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
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }
            Movie movie = movieRepo.Get(id);

            if (movie == null)
            {
                return NotFound();
            }

            movieRepo.Delete(movie);

            return Ok(movie);
        }

        //Add an actor to the movie
        [HttpPut("{idM}/actors/{idA}")]
        public IActionResult addActorToMovie(int idM, int idA)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }

            if(movieRepo.Get(idM)==null || actorRepo.Get(idA) == null)
            {
                return NotFound();
            }

            ActorMovie actorMovie = new ActorMovie();
            actorMovie.Actor = actorRepo.Get(idA);
            actorMovie.Movie = movieRepo.Get(idM);

            actorMovieRepo.Insert(actorMovie);

            return Ok(actorMovie);
        }

        [HttpGet("{id}/actors")]
        public IActionResult viewActorsForMovie(int id)
        {
            if (movieRepo.Get(id) == null)
            {
                return NotFound();
            }

            List <Actor> actorsList = new List<Actor>();

            foreach (var actorMovie in movieRepo.Get(id).Actors)
            {
                if(actorRepo.Get(actorMovie.ActorId) == null)
                {
                    return NotFound();
                }

                Actor actor = actorRepo.Get(actorMovie.ActorId);
                actorsList.Add(actor);
            }

            return Ok(actorsList);

        }
    }
}
