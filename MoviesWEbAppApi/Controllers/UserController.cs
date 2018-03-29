using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels;
using MoviesWEbAppApi.BindModels.UserBindModels;
using MoviesWEbAppApi.wwwroot.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserRepository userRepo = new UserRepository();
        MovieRepository movieRepo = new MovieRepository();

        //Get All Users
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            {
                return Unauthorized();
            }
            List<UserViewModel> usersModel = new List<UserViewModel>();
            foreach (User user in userRepo.GetAll())
            {
                usersModel.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    EMail = user.EMail,
                    AvatarUrl = user.AvatarUrl,
                    IsAdmin = user.IsAdmin,
                    WatchedMovies = user.WatchedMovies
                });
            }
            return Ok(usersModel);
        }


        //Get User by Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
                !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            {
                return Unauthorized();
            }

            if (userRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            return Ok(userRepo.Get(a => a.Id == id));
        }
        //Register User
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]RegisterUserEntryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = new User(model.Username, model.Password, model.EMail, model.AvatarUrl);

            userRepo.Insert(user);
            return Ok(user);
        }
        // Edit User
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }

            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
                !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            {
                return Unauthorized();
            }

            if (userRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            User user = userRepo.Get(a => a.Id == id);
            user.Username = model.Username;
            user.Password = model.Password;
            user.IsAdmin = model.IsAdmin;
            user.WatchedMovies = model.WatchedMovies;
            user.EMail = model.EMail;
            user.AvatarUrl = model.AvatarUrl;

            userRepo.Update(user);
            HttpContext.Session.SetObjectAsJson<User>("loggedUser", user);

            return Ok(user);
        }

        //Delete User
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
               !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            {
                return Unauthorized();
            }

            User user = new User();
            user = userRepo.Get(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            userRepo.Delete(user);

            return NoContent();
        }

        //Login 
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginEntryModel model)
        {
            User loggedUser = userRepo.Get(a => a.EMail == model.EMail && a.Password == model.password);

            if (loggedUser == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetObjectAsJson<User>("loggedUser", loggedUser);

            return Ok(loggedUser);

        }

        //Add a movie to the logged user's watched list
        [HttpPut("movies/{id}")]
        public IActionResult AddMovieToWL(string id)
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }

            User user = HttpContext.Session.GetObjectFromJson<User>("loggedUser");

            if (movieRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }
            user.WatchedMovies.Add(movieRepo.Get(a => a.Id == id));
            HttpContext.Session.SetObjectAsJson<User>("loggedUser", user);

            return Ok(user.WatchedMovies);
        }

        [HttpGet("movies")]
        public IActionResult viewWatchedMovies()
        {
            if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }

            User user = HttpContext.Session.GetObjectFromJson<User>("loggedUser");
            List<UserMovieViewModel> watchedMoviesList = new List<UserMovieViewModel>();
            foreach (var movie in user.WatchedMovies)
            {
                watchedMoviesList.Add(new UserMovieViewModel
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

            return Ok(watchedMoviesList);
        }
    }
}
