using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Data.Entity.Entities.LogService;
using LogService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels;
using MoviesWEbAppApi.BindModels.UserBindModels;
using MoviesWEbAppApi.wwwroot.Extensions;
using NotificationService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ILog _logger = Logger.GetInstance;
        UserRepository userRepo = new UserRepository();
        MovieRepository movieRepo = new MovieRepository();
        private NotificationManager _notificationManager = new NotificationManager();

        //Get All Users
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            //{
            //    return Unauthorized();
            //}
            //if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin)
            //{
            //    return Unauthorized();
            //}
            List<UserViewModel> usersModel = new List<UserViewModel>();
            try
            {
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
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(usersModel);
        }


        //Get User by Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            //if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
            //    !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            //{
            //    return Unauthorized();
            //}

            if (userRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            try
            {
                userRepo.Get(a => a.Id == id);
            }

            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(userRepo.Get(a => a.Id == id));
        }
        //Register User
        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RegisterUserEntryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user;
            try
            {
                user = new User(model.Username, model.Password, model.EMail, model.AvatarUrl);
                userRepo.Insert(user);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(user);
        }
        // Edit User
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
            //{
            //    return Unauthorized();
            //}
            //if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
            //    !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            //{
            //    return Unauthorized();
            //}

            if (userRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            User user = userRepo.Get(a => a.Id == id);

            try
            {
                user.Username = model.Username;
                user.Password = model.Password;
                user.IsAdmin = model.IsAdmin;
                user.WatchedMovies = model.WatchedMovies;
                user.EMail = model.EMail;
                user.AvatarUrl = model.AvatarUrl;

                userRepo.Update(user);

                await _notificationManager.SendEmailAsync(
                    user.EMail,
                    "your moviesWebApp account", 
                    "there have been changes" +
                    "made to your moviesWebApp account on " 
                    + DateTime.Now + 
                    " if it wasnt you ... please contact our " +
                    "user support ASAP");
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            HttpContext.Session.SetObjectAsJson<User>("loggedUser", user);

            return Ok(user);
        }

        //Delete User
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {

            //if (!HttpContext.Session.GetObjectFromJson<User>("loggedUser").IsAdmin &&
            //   !HttpContext.Session.GetObjectFromJson<User>("loggedUser").Id.Equals(id))
            //{
            //    return Unauthorized();
            //}

            User user;
            try
            {
                user = userRepo.Get(a => a.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                userRepo.Delete(user);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(user);
        }

        ////Login 
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] UserLoginEntryModel model)
        //{
        //    User loggedUser = userRepo.Get(a => a.EMail == model.EMail && a.Password == model.password);

        //    if (loggedUser == null)
        //    {
        //        return NotFound();
        //    }

        //    HttpContext.Session.SetObjectAsJson<User>("loggedUser", loggedUser);

        //    return Ok(loggedUser);

        //}

        ////Add a movie to the logged user's watched list
        //[HttpPut("movies/{id}")]
        //public IActionResult AddMovieToWL(string id)
        //{
        //    if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
        //    {
        //        return Unauthorized();
        //    }

        //    User user = HttpContext.Session.GetObjectFromJson<User>("loggedUser");

        //    if (movieRepo.Get(a => a.Id == id) == null)
        //    {
        //        return NotFound();
        //    }
        //    user.WatchedMovies.Add(movieRepo.Get(a => a.Id == id));
        //    HttpContext.Session.SetObjectAsJson<User>("loggedUser", user);

        //    return Ok(user.WatchedMovies);
        //}

        //    [HttpGet("movies")]
        //    public IActionResult viewWatchedMovies()
        //    {
        //        if (HttpContext.Session.GetObjectFromJson<User>("loggedUser") == null)
        //        {
        //            return Unauthorized();
        //        }

        //        User user = HttpContext.Session.GetObjectFromJson<User>("loggedUser");
        //        List<UserMovieViewModel> watchedMoviesList = new List<UserMovieViewModel>();
        //        foreach (var movie in user.WatchedMovies)
        //        {
        //            watchedMoviesList.Add(new UserMovieViewModel
        //            {
        //                Name = movie.Name,
        //                ReleaseDate = movie.ReleaseDate,
        //                imgUrl = movie.imgUrl,
        //                MovieIMDBScore = movie.MovieIMDBScore,
        //                MovieIMDBUrl = movie.MovieIMDBUrl,
        //                MovieRottenTomatoesScore = movie.MovieRottenTomatoesScore,
        //                MovieRottenTomatoesUrl = movie.MovieRottenTomatoesUrl,
        //                MovieDesc = movie.MovieDesc
        //            });
        //        }

        //        return Ok(watchedMoviesList);
        //    }
        //}
    }
}
