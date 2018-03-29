using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Data.Entity.Entities.LogService;
using LogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels.MovieBindModels;
using MoviesWEbAppApi.wwwroot.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private ILog _logger = Logger.GetInstance;
        MovieRepository movieRepo = new MovieRepository();
        ActorRepository actorRepo = new ActorRepository();
        ActorMovieRepository actorMovieRepo = new ActorMovieRepository();

        //Get All Movies
        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<MovieViewModel> moviesModel = new List<MovieViewModel>();

            try
            {
                foreach (var movie in movieRepo.GetAll())
                {
                    moviesModel.Add(new MovieViewModel
                    {
                        Id = movie.Id,
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
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(moviesModel);
        }
        // Get Movie By Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            try
            {
                movieRepo.Get(a => a.Id == id);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(movieRepo.Get(a => a.Id == id));
        }
        //Add Movie
        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MovieViewModel model)
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

            Movie movie;
            try
            {
                movie = new Movie(
                    model.Name,
                    model.ReleaseDate,
                    model.imgUrl,
                    model.MovieIMDBScore,
                    model.MovieIMDBUrl,
                    model.MovieRottenTomatoesScore,
                    model.MovieRottenTomatoesUrl,
                    model.MovieDesc);

                movieRepo.Insert(movie);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(movie);

        }
        //Edit Movie
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]MovieViewModel model)
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
            try
            {
                movie.Name = model.Name;
                movie.ReleaseDate = model.ReleaseDate;
                movie.imgUrl = model.imgUrl;
                movie.MovieIMDBScore = model.MovieIMDBScore;
                movie.MovieIMDBUrl = model.MovieIMDBUrl;
                movie.MovieRottenTomatoesScore = model.MovieRottenTomatoesScore;
                movie.MovieRottenTomatoesUrl = model.MovieRottenTomatoesUrl;
                movie.MovieDesc = model.MovieDesc;

                movieRepo.Update(movie);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(movieRepo);

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
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

            try
            {
                movieRepo.Delete(movie);
            }
            catch(Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
           
            return Ok(movie);
        }

        //Add an actor to the movie
        [HttpPut("{idM}/actors/{idA}")]
        public async Task<IActionResult> addActorToMovieAsync(string idM, string idA)
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

            try
            {
            actorMovie.Actor = actorRepo.Get(a => a.Id == idA);
            actorMovie.Movie = movieRepo.Get(a => a.Id == idM);

            actorMovieRepo.Insert(actorMovie);
            }
            catch(Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(actorMovie);
        }

        [HttpGet("{id}/actors")]
        public async Task<IActionResult> viewActorsForMovieAsync(string id)
        {
            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            List<Actor> actorsList = new List<Actor>();

            try
            {
                foreach (var actorMovie in actorMovieRepo.GetAll())
                {
                    if (actorMovie.MovieId == id)
                    {
                        Actor actor = actorRepo.Get(a => a.Id == actorMovie.ActorId);
                        actorsList.Add(actor);
                    }
                }
            }
            catch(Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(actorsList);

        }
    }
}
