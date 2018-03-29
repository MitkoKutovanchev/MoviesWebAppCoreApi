using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Data.Entity.Entities.LogService;
using LogService;
using Microsoft.AspNetCore.Mvc;
using MoviesWEbAppApi.BindModels;
using MoviesWEbAppApi.wwwroot.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesWEbAppApi.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : Controller
    {

        private ILog _logger = Logger.GetInstance;

        ActorRepository actorRepo = new ActorRepository();
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<ActorViewModel> actorsModel = new List<ActorViewModel>();

            try
            {
                foreach (var actor in actorRepo.GetAll())
                {
                    actorsModel.Add(new ActorViewModel
                    {
                        Id = actor.Id,
                        FirstName = actor.FirstName,
                        LastName = actor.LastName
                    });
                }

            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(actorsModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (actorRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }
            try
            {
                actorRepo.Get(a => a.Id == id);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }
            return Ok(actorRepo.Get(a => a.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ActorViewModel model)
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
            Actor actor;
            try
            {
                actor = new Actor(model.FirstName, model.LastName);

                actorRepo.Insert(actor);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }


            return Ok(actor);

        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]ActorViewModel model)
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

            if (actorRepo.Get(a => a.Id == id) == null)
            {
                return NotFound();
            }

            Actor actor;
            try
            {
                actor = actorRepo.Get(a => a.Id == id);
                actor.FirstName = model.FirstName;
                actor.LastName = model.LastName;

                actorRepo.Update(actor);
            }
            catch(Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(actor);

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
            Actor actor;
            try
            {
                actor = actorRepo.Get(a => a.Id == id);

                if (actor == null)
                {
                    return NotFound();
                }

                actorRepo.Delete(actor);

            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                return BadRequest();
            }

            return Ok(actor);
        }
    }
}
