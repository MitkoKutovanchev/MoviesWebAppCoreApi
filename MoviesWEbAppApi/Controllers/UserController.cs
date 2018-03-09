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
            List<UserBindModel> usersModel = new List<UserBindModel>();
            foreach (User user in userRepo.GetAll())
            {
                usersModel.Add(new UserBindModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    EMail = user.EMail,
                    AvatarUrl = user.AvatarUrl,
                    IsAdmin = user.IsAdmin,
                    WatchedMovies = user.WatchedMovies,
                    apiIdPlaceholder = user.apiIdPlaceholder
                });
            }
            return Ok(usersModel);
        }


        //Get User by Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
                HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id != id)
            {
                return Unauthorized();
            }

            if (userRepo.Get(id)==null)
            {
                return NotFound();
            }

            return Ok(userRepo.Get(id));
        }
        //Register User
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]RegisterUserBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = new User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.EMail = model.EMail;

            if (model.AvatarUrl != null)
            {
                user.AvatarUrl = model.AvatarUrl;
            }

            userRepo.Insert(user);
            return Ok(user);
        }
        // Edit User
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserBindModel model)
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
                HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id != id)
            {
                return Unauthorized();
            }

            if (userRepo.Get(id) == null)
            {
                return NotFound();
            }

            User user = userRepo.Get(id);
            user.Username = model.Username;
            user.Password = model.Password;
            user.IsAdmin = model.IsAdmin;
            user.WatchedMovies = model.WatchedMovies;
            user.EMail = model.EMail;
            user.AvatarUrl = model.AvatarUrl;
            user.apiIdPlaceholder = model.apiIdPlaceholder;

            userRepo.Update(user);
            HttpContext.Session.SetObjectAsJson<User>("loggedUser", user);

            return Ok(user);
        }

        //Delete User
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
               HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id != id)
            {
                return Unauthorized();
            }

            User user = new User();
            user = userRepo.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            userRepo.Delete(user);

            return NoContent();
        }

        //Login 
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginBindModel model)
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
        public IActionResult AddMovieToWL(int id)
        {
            if(HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            {
                return Unauthorized();
            }

            User user = HttpContext.Session.GetObjectFromJson<User>("loggedUser");

            if (movieRepo.Get(id) == null)
            {
                return NotFound();
            }
            user.WatchedMovies.Add(movieRepo.Get(id));
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
            List<UserMovieBindModel> watchedMoviesList = new List<UserMovieBindModel>();
            foreach (var movie in user.WatchedMovies)
            {
                watchedMoviesList.Add(new UserMovieBindModel
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
